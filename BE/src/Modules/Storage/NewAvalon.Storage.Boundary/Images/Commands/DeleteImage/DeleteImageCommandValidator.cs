using FluentValidation;

namespace NewAvalon.Storage.Boundary.Images.Commands.DeleteImage
{
    public sealed class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator() => RuleFor(x => x.ImageId).NotEmpty();
    }
}
