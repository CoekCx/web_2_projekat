using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Domain.Repositories
{
    public interface IImageRepository
    {
        Task<Image> GetByIdAsync(ImageId imageId, CancellationToken cancellationToken);

        void Insert(Image image);

        void Remove(Image image);
    }
}
