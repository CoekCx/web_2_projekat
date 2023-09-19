using MassTransit;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Exceptions.Products;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Order.Boundary.Orders.Commands.CreateOrders;
using NewAvalon.Order.Business.Contracts.Products;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Exceptions;
using NewAvalon.Order.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Commands.CreateOrders
{
    internal sealed class CreateOrdersCommandHandler : ICommandHandler<CreateOrdersCommand, List<EntityCreatedResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IRequestClient<IGetCatalogProductListRequest> _catalogProductListRequest;
        private readonly ISystemTime _systemTime;

        public CreateOrdersCommandHandler(
            IOrderRepository orderRepository,
            IOrderUnitOfWork unitOfWork,
            IRequestClient<IGetCatalogProductListRequest> catalogProductListRequest,
            ISystemTime systemTime)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _catalogProductListRequest = catalogProductListRequest;
            _systemTime = systemTime;
        }

        public async Task<List<EntityCreatedResponse>> Handle(CreateOrdersCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.Products.Select(x => x.Id).ToArray();

            var catalogProductsRequest = new GetCatalogProductListRequest
            {
                ProductIds = productIds
            };

            var catalogProductListResponse = (
                await _catalogProductListRequest.GetResponse<IGetCatalogProductListResponse>(
                    catalogProductsRequest,
                    cancellationToken))
                .Message;

            var productIdNotFoundInCatalog = productIds
                .Except(
                    catalogProductListResponse.Products.Select(x => x.Id))
                .FirstOrDefault();

            if (productIdNotFoundInCatalog != Guid.Empty)
            {
                throw new ProductNotFoundException(productIdNotFoundInCatalog);
            }

            var productsPerDealer = catalogProductListResponse
                .Products
                .GroupBy(x => x.CreatorId)
                .ToDictionary(x => x.Key, x => x.ToList());

            var response = new List<EntityCreatedResponse>();

            foreach (var productDealerPair in productsPerDealer)
            {
                var deliveryOnUtc = _systemTime.UtcNow.AddHours(1 + RandomNumberGenerator.GetInt32(1, 4));

                var order = new Domain.Entities.Order(new OrderId(
                    Guid.NewGuid()),
                    request.OwnerId,
                    productDealerPair.Key,
                    request.Comment,
                    request.DeliveryAddress,
                    deliveryOnUtc);

                response.Add(new EntityCreatedResponse(order.Id.Value));

                _orderRepository.Insert(order);

                foreach (var catalogProduct in productDealerPair.Value)
                {
                    var requestedProduct = request.Products.FirstOrDefault(x => x.Id == catalogProduct.Id);

                    if (catalogProduct.Quantity < requestedProduct.Quantity)
                    {
                        throw new ProductInvalidQuantityException(requestedProduct.Id);
                    }

                    order.AddProduct(catalogProduct.Id, catalogProduct.Price, requestedProduct.Quantity, catalogProduct.Name);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
