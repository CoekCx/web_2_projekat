using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Domain.Repositories
{
    public interface IClientRepository
    {
        void Insert(Client client);

        Task<Client> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);
    }
}
