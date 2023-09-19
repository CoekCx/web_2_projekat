using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Infrastructure.Options;
using System;

namespace NewAvalon.Storage.Infrastructure.Services
{
    internal sealed class FileUrlFormatter : IFileUrlFormatter, ITransient
    {
        private readonly StorageBucketOptions _imageStorageBucketOptions;

        public FileUrlFormatter(IOptions<StorageBucketOptions> options) => _imageStorageBucketOptions = options.Value;

        public Uri Format(Guid fileId) =>
            new($"https://{_imageStorageBucketOptions.BucketName}.{_imageStorageBucketOptions.AwsS3StorageUrl}/files/{fileId}");
    }
}
