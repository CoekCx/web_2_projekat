using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Domain.Repositories
{
    public interface IDealerRepository
    {
        void Insert(Dealer dealer);

        Task<Dealer> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);
    }
}
