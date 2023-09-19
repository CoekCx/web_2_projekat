using FluentValidation;

namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProductImage
{
    public sealed class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
    {
        public UpdateProductImageCommandValidator() => RuleFor(x => x.UserId).NotEmpty();
    }
}
