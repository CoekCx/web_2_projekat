using Microsoft.EntityFrameworkCore;
using NewAvalon.UserAdministration.Business.Permissions.Consumers;
using NewAvalon.UserAdministration.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.DataRequests.Permissions
{
    internal sealed class GetPermissionsDataRequest : IGetPermissionsDataRequest
    {
        private readonly UserAdministrationDbContext _dbContext;

        public GetPermissionsDataRequest(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<string[]> GetAsync(GetPermissionsRequest request, CancellationToken cancellationToken = default)
        {
            var permissionNames = await (
                    from user in _dbContext.Set<User>()
                    let permissions = user.Roles.SelectMany(role => role.Permissions).ToList()
                    where user.Id == request.UserId
                    select new
                    {
                        PermissionNames = permissions.Select(x => x.Name)
                    })
                .ToArrayAsync(cancellationToken);

            string[] distinctPermissionNames = permissionNames
                .SelectMany(x => x.PermissionNames)
                .Distinct()
                .ToArray();

            return distinctPermissionNames;
        }
    }
}
