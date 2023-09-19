using Microsoft.AspNetCore.Http;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Infrastructure.Abstractions;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Infrastructure.Services
{
    internal sealed class FileStorageService : IFileStorageService, ITransient
    {
        private readonly IStorageService _storageService;

        public FileStorageService(IStorageService storageService) => _storageService = storageService;

        public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var fileId = Guid.NewGuid();

            await _storageService.UploadAsync(file, FormatKey(fileId), cancellationToken);

            return fileId;
        }

        public async Task<Guid> UploadAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            var fileId = Guid.NewGuid();

            await using var memoryStream = new MemoryStream(bytes);

            await _storageService.UploadAsync(memoryStream, FormatKey(fileId), cancellationToken);

            return fileId;
        }

        public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default) =>
            await _storageService.DeleteAsync(FormatKey(fileId), cancellationToken);

        public async Task<bool> ExistsAsync(Guid fileId, CancellationToken cancellationToken = default) =>
            await _storageService.ExistsAsync(FormatKey(fileId), cancellationToken);

        public async Task<byte[]> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default) =>
            await _storageService.DownloadAsync(FormatKey(fileId), cancellationToken);

        private static string FormatKey(Guid fileId) => $"files/{fileId}";
    }
}
