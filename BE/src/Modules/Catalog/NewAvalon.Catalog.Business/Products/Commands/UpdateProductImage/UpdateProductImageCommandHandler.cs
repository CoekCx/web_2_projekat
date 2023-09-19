using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Commands.UpdateProductImage;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Catalog.Domain.ValueObjects;
using NewAvalon.Domain.Exceptions.Images;
using NewAvalon.Domain.Exceptions.Products;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.UserAdministration.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Commands.UpdateProductImage
{
    internal sealed class UpdateProductImageCommandHandler : ICommandHandler<UpdateProductImageCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public UpdateProductImageCommandHandler(
            IImageService imageService,
            IProductRepository productRepository,
            ICatalogUnitOfWork unitOfWork)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            IImageResponse imageResponse = null;
            if (request.ImageId.HasValue && !(imageResponse = await _imageService.GetByIdAsync(request.ImageId.Value, cancellationToken)).Exists)
            {
                throw new ImageNotFoundException(request.ImageId.Value);
            }


            Product product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

            if (product.CreatorId != request.UserId)
            {
                throw new ProductNotFoundException(request.ProductId);
            }
            var productImage = ProductImage.Create(imageResponse?.ImageId, imageResponse?.ImageUrl);

            product.ChangeProductImage(productImage);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
