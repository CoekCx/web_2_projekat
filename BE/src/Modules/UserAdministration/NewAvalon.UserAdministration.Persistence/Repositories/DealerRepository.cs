using Microsoft.EntityFrameworkCore;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.Repositories
{
    internal sealed class DealerRepository : IDealerRepository
    {
        private readonly UserAdministrationDbContext _dbContext;

        public DealerRepository(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public void Insert(Dealer dealer) => _dbContext.Set<Dealer>().Add(dealer);

        public async Task<Dealer> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Dealer>()
                .FirstOrDefaultAsync(dealer => dealer.Id == userId, cancellationToken);
    }
}