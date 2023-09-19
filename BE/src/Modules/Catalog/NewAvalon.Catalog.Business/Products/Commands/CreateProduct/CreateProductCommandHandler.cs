using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Commands.CreateProduct
{
    internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, EntityCreatedResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            ICatalogUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EntityCreatedResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(
                new Domain.EntityIdentifiers.ProductId(Guid.NewGuid()),
                request.Name,
                request.Price,
                request.Capacity,
                request.Description,
                request.CreatorId);

            _productRepository.Insert(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new EntityCreatedResponse(product.Id.Value);
        }
    }
}
