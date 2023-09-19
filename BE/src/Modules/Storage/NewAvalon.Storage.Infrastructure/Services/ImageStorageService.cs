using Microsoft.AspNetCore.Http;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Infrastructure.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Infrastructure.Services
{
    internal sealed class ImageStorageService : IImageStorageService, ITransient
    {
        private readonly IStorageService _storageService;

        public ImageStorageService(IStorageService storageService) => _storageService = storageService;

        public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var imageId = Guid.NewGuid();

            await _storageService.UploadAsync(file, FormatKey(imageId), cancellationToken);

            return imageId;
        }

        public async Task DeleteAsync(Guid imageId, CancellationToken cancellationToken = default) =>
            await _storageService.DeleteAsync(FormatKey(imageId), cancellationToken);

        public async Task<bool> ExistsAsync(Guid imageId, CancellationToken cancellationToken = default) =>
            await _storageService.ExistsAsync(FormatKey(imageId), cancellationToken);

        private static string FormatKey(Guid imageId) => $"images/{imageId}";
    }
}
