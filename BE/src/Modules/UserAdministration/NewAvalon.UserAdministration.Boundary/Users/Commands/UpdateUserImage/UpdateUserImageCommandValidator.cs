using FluentValidation;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUserImage
{
    public sealed class UpdateUserImageCommandValidator : AbstractValidator<UpdateUserImageCommand>
    {
        public UpdateUserImageCommandValidator() => RuleFor(x => x.Id).NotEmpty();
    }
}
