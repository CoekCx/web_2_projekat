using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Abstractions
{
    public interface IImageStorageService
    {
        Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid imageId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid imageId, CancellationToken cancellationToken = default);
    }
}
