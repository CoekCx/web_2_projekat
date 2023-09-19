using Microsoft.EntityFrameworkCore;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.Repositories
{
    internal sealed class ClientRepository : IClientRepository
    {
        private readonly UserAdministrationDbContext _dbContext;

        public ClientRepository(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public void Insert(Client client) => _dbContext.Set<Client>().Add(client);

        public async Task<Client> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Client>()
                .FirstOrDefaultAsync(client => client.Id == userId, cancellationToken);
    }
}