using MassTransit;
using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Commands.DeleteProduct;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Exceptions;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Domain.Exceptions.Products;
using NewAvalon.Messaging.Contracts.Products;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;
        private readonly IRequestClient<IIsProductUsedRequest> _isProductUsedRequestClient;

        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            ICatalogUnitOfWork unitOfWork,
            IRequestClient<IIsProductUsedRequest> isProductUsedRequestClient)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _isProductUsedRequestClient = isProductUsedRequestClient;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

            if (product is null || product.CreatorId != request.CreatorId)
            {
                throw new ProductNotFoundException(request.ProductId);
            }

            var isProductUsedRequest = new IsProductUsedRequest()
            {
                ProductId = product.Id.Value
            };

            var isProductUsedResponse = await _isProductUsedRequestClient.GetResponse<IIsProductUsedResponse>(isProductUsedRequest, cancellationToken);

            if (isProductUsedResponse.Message.IsCurrentlyInUse)
            {
                throw new ProductCannotBeDeletedException(product.Id.Value);
            }

            if (isProductUsedResponse.Message.IsUsed)
            {
                product.Deactivate();
            }
            else
            {
                _productRepository.Delete(product);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private sealed class IsProductUsedRequest : IIsProductUsedRequest
        {
            public Guid ProductId { get; set; }
        }
    }
}
