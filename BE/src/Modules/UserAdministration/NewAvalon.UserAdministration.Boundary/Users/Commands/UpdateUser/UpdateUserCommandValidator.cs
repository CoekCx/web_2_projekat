using FluentValidation;
using NewAvalon.Boundary.Extensions;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).FirstName();

            RuleFor(x => x.LastName).LastName();
        }
    }
}

