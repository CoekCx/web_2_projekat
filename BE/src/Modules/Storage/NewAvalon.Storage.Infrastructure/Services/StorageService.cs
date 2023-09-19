using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Storage.Infrastructure.Abstractions;
using NewAvalon.Storage.Infrastructure.Options;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Infrastructure.Services
{
    internal sealed class StorageService : IStorageService, ITransient
    {
        private const string KeyDoesNotExistErrorMessage = "The specified key does not exist.";

        private readonly IAmazonS3 _amazonS3Client;
        private readonly StorageBucketOptions _imageStorageBucketOptions;

        public StorageService(IAmazonS3 amazonS3Client, IOptions<StorageBucketOptions> storageBucketOptions)
        {
            _amazonS3Client = amazonS3Client;
            _imageStorageBucketOptions = storageBucketOptions.Value;
        }

        public async Task UploadAsync(IFormFile file, string key, CancellationToken cancellationToken = default)
        {
            var transferUtilityUploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = _imageStorageBucketOptions.BucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                AutoCloseStream = true
            };

            using var transferUtility = new TransferUtility(_amazonS3Client);

            await transferUtility.UploadAsync(transferUtilityUploadRequest, cancellationToken);
        }

        public async Task UploadAsync(Stream stream, string key, CancellationToken cancellationToken = default)
        {
            var transferUtilityUploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = _imageStorageBucketOptions.BucketName,
                Key = key,
                InputStream = stream,
                AutoCloseStream = true
            };

            using var transferUtility = new TransferUtility(_amazonS3Client);

            await transferUtility.UploadAsync(transferUtilityUploadRequest, cancellationToken);
        }

        public async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _imageStorageBucketOptions.BucketName,
                Key = key
            };

            await _amazonS3Client.DeleteObjectAsync(deleteObjectRequest, cancellationToken);
        }

        public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                GetObjectResponse getObjectResponse = await _amazonS3Client
                    .GetObjectAsync(_imageStorageBucketOptions.BucketName, key, cancellationToken);

                return getObjectResponse?.HttpStatusCode is HttpStatusCode.OK;
            }
            catch (AmazonS3Exception amazonS3Exception) when (amazonS3Exception.Message == KeyDoesNotExistErrorMessage)
            {
                return false;
            }
        }

        public async Task<byte[]> DownloadAsync(string key, CancellationToken cancellationToken = default)
        {
            var memoryStream = new MemoryStream();
            using (GetObjectResponse getObjectResponse = await _amazonS3Client.GetObjectAsync(_imageStorageBucketOptions.BucketName, key, cancellationToken))
            {
                await getObjectResponse.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
            }

            return memoryStream.ToArray();
        }
    }
}
