using FluentValidation;

namespace NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Price).Must(x => x > 0);

            RuleFor(x => x.Capacity).Must(x => x > 0);
        }
    }
}

