using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Domain.Repositories
{
    public interface IUserAdministrationUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
