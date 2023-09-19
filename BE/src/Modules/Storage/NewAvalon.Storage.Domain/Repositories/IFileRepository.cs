using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Domain.Repositories
{
    public interface IFileRepository
    {
        Task<File[]> GetByIdsAsync(FileId[] fileIds, CancellationToken cancellationToken);

        Task<File> GetByIdAsync(FileId fileId, CancellationToken cancellationToken);

        void Insert(File file);

        void Remove(File file);
    }
}
