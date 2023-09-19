using FluentValidation;

namespace NewAvalon.Storage.Boundary.Files.Commands.DeleteFile
{
    public sealed class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator() => RuleFor(x => x.FileId).NotEmpty();
    }
}
