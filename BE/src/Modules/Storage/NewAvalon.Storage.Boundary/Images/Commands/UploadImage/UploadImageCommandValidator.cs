using FluentValidation;
using System;
using System.Linq;

namespace NewAvalon.Storage.Boundary.Images.Commands.UploadImage
{
    public sealed class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        private const long FiveMegabytes = 1024 * 1024 * 5;
        private static readonly string[] SupportedImageExtensions = { "jpg", "jpeg", "png" };

        public UploadImageCommandValidator()
        {
            RuleFor(x => x.Image).NotNull();

            RuleFor(x => x.Image)
                .Must(f => SupportedImageExtensions.Any(imageExtension => f.FileName.EndsWith(imageExtension, StringComparison.OrdinalIgnoreCase)))
                .When(x => x.Image is not null)
                .WithMessage("The image extension is not supported.");

            RuleFor(x => x.Image)
                .Must(f => f.Length <= FiveMegabytes)
                .When(x => x.Image is not null)
                .WithMessage("The image size is larger than 5MB.");
        }
    }
}
