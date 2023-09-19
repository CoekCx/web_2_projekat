using Microsoft.EntityFrameworkCore;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Persistence.Repositories
{
    internal sealed class ImageRepository : IImageRepository
    {
        private readonly StorageDbContext _dbContext;

        public ImageRepository(StorageDbContext dbContext) => _dbContext = dbContext;

        public async Task<Image> GetByIdAsync(ImageId imageId, CancellationToken cancellationToken) =>
            await _dbContext.Set<Image>().FirstOrDefaultAsync(image => image.Id == imageId, cancellationToken);

        public void Insert(Image image) => _dbContext.Set<Image>().Add(image);

        public void Remove(Image image) => _dbContext.Set<Image>().Remove(image);
    }
}
