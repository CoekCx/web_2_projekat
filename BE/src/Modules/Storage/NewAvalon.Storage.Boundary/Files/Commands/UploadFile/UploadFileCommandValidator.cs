using FluentValidation;

namespace NewAvalon.Storage.Boundary.Files.Commands.UploadFile
{
    public sealed class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator() => RuleFor(x => x.File).NotNull();
    }
}
