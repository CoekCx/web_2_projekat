using FluentValidation;

namespace NewAvalon.Order.Boundary.Orders.Commands.CreateOrders
{
    public sealed class CreateOrdersCommandValidator : AbstractValidator<CreateOrdersCommand>
    {
        public CreateOrdersCommandValidator()
        {
            RuleFor(x => x.Products).NotEmpty();

            RuleForEach(x => x.Products)
                .ChildRules(product => product.RuleFor(x =>
                    x.Quantity % 1 == 0 &&
                    x.Quantity > 0));
        }
    }
}

