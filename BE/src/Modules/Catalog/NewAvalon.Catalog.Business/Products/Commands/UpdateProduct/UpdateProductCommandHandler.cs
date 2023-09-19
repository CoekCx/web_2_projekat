using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Domain.Exceptions.Products;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            ICatalogUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

            if (product is null || product.CreatorId != request.CreatorId)
            {
                throw new ProductNotFoundException(request.ProductId);
            }

            product.Update(request.Name, request.Price, request.Capacity, request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
