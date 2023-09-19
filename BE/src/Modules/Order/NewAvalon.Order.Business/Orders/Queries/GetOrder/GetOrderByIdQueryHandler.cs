using MassTransit;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Boundary.Orders.Queries.GetOrder;
using NewAvalon.Order.Business.Contracts.Users;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Exceptions;
using NewAvalon.Order.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Queries.GetOrder
{
    internal sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDetailsResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRequestClient<IUserDetailsListRequest> _userDetailsListRequest;

        public GetOrderByIdQueryHandler(
            IOrderRepository orderRepository,
            IRequestClient<IUserDetailsListRequest> userDetailsListRequest)
        {
            _orderRepository = orderRepository;
            _userDetailsListRequest = userDetailsListRequest;
        }

        public async Task<OrderDetailsResponse> Handle(
            GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId), cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(request.OrderId);
            }

            if (order.OwnerId != request.UserId && order.DealerId != request.UserId)
            {
                throw new OrderNotFoundException(request.OrderId);
            }

            var userDetailsListRequest = new UserDetailsListRequest()
            {
                UserIds = new[] { order.OwnerId, order.DealerId }
            };

            var userDetailsListResponse = (
                    await _userDetailsListRequest.GetResponse<IUserDetailsListResponse>(
                        userDetailsListRequest,
                        cancellationToken))
                .Message;

            var owner = userDetailsListResponse.Users.First(x => x.Id == order.OwnerId);
            var dealer = userDetailsListResponse.Users.First(x => x.Id == order.DealerId);

            return new OrderDetailsResponse(
                order.Id.Value,
                new OrderUserDetailsResponse(
                    owner.Id,
                    owner.FirstName,
                    owner.LastName,
                    owner.Username),
                new OrderUserDetailsResponse(
                    dealer.Id,
                    dealer.FirstName,
                    dealer.LastName,
                    dealer.Username),
                order.GetName(),
                order.Comment,
                order.DeliveryAddress,
                (int)order.Status,
                order.Products.Select(product => new ProductDetailsResponse(
                    product.Id.Value,
                    product.OrderId.Value,
                    product.CatalogProductId,
                    product.Quantity,
                    product.Price,
                    product.GetFullPrice(),
                    product.Name)).ToList(),
                Domain.Entities.Order.GetDeliveryPrice(),
                order.GetFullPrice(),
                order.DeliveryOnUtc);
        }
    }
}
