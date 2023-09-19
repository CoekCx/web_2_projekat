using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);

        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(UserId userId, CancellationToken cancellationToken = default);

        Task<bool> IsEmailTakenAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> IsUserNameTakenAsync(string userName, CancellationToken cancellationToken = default);

        Task<User> GetByImageIdAsync(Guid imageId, CancellationToken cancellationToken = default);

        void Insert(User user);

        void Delete(User user);
    }
}
