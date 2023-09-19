using Microsoft.EntityFrameworkCore;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Business.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using NewAvalon.UserAdministration.Boundary.Contracts.Users;

namespace NewAvalon.UserAdministration.Persistence.DataRequests.Users
{
    internal sealed class GetUserByIdDataRequest : IGetUserByIdDataRequest
    {
        private readonly UserAdministrationDbContext _dbContext;

        public GetUserByIdDataRequest(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<UserDetailsResponse> GetAsync(UserId request, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<User>()
                .Include(user => user.Roles)
                .Where(user => user.Id == request)
                .Select(user => new UserDetailsResponse(
                    user.Id.Value,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Roles.Select(x => new RoleResponse(x.Id.Value, x.Description)).ToList()))
                .FirstOrDefaultAsync(cancellationToken);
    }
}
