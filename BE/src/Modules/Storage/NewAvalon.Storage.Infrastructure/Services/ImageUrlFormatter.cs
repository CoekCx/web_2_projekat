using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Infrastructure.Options;
using System;

namespace NewAvalon.Storage.Infrastructure.Services
{
    internal sealed class ImageUrlFormatter : IImageUrlFormatter, ITransient
    {
        private readonly StorageBucketOptions _imageStorageBucketOptions;

        public ImageUrlFormatter(IOptions<StorageBucketOptions> options) => _imageStorageBucketOptions = options.Value;

        public Uri Format(Guid imageId) =>
            new($"https://{_imageStorageBucketOptions.BucketName}.{_imageStorageBucketOptions.AwsS3StorageUrl}/images/{imageId}");
    }
}
