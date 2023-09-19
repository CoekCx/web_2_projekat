using FluentValidation;

namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Price).Must(x => x > 0);

            RuleFor(x => x.Capacity).Must(x => x >= 0);
        }
    }
}

