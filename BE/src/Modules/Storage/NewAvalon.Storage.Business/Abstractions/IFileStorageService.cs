using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Abstractions
{
    public interface IFileStorageService
    {
        Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);

        Task<Guid> UploadAsync(byte[] bytes, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid fileId, CancellationToken cancellationToken = default);

        Task<byte[]> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);
    }
}
