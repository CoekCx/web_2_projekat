using NewAvalon.Messaging.Contracts.Images;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Abstractions
{
    public interface IImageService
    {
        Task<IImageResponse> GetByIdAsync(Guid imageId, CancellationToken cancellationToken);
    }
}
