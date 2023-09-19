using Microsoft.EntityFrameworkCore;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Persistence.Repositories
{
    internal sealed class FileRepository : IFileRepository
    {
        private readonly StorageDbContext _dbContext;

        public FileRepository(StorageDbContext dbContext) => _dbContext = dbContext;

        public async Task<File[]> GetByIdsAsync(FileId[] fileIds, CancellationToken cancellationToken) =>
            await _dbContext.Set<File>()
                .AsNoTracking()
                .Where(file => fileIds.Contains(file.Id))
                .ToArrayAsync(cancellationToken);

        public async Task<File> GetByIdAsync(FileId fileId, CancellationToken cancellationToken) =>
            await _dbContext.Set<File>().FirstOrDefaultAsync(image => image.Id == fileId, cancellationToken);

        public void Insert(File file) => _dbContext.Set<File>().Add(file);

        public void Remove(File file) => _dbContext.Set<File>().Remove(file);
    }
}
