using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Business.Users.Queries.GetPendingUsers;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NewAvalon.UserAdministration.Boundary.Contracts.Users;

namespace NewAvalon.UserAdministration.Persistence.DataRequests.Users
{
    internal sealed class GetPendingUsersDataRequest : IGetPendingUsersDataRequest
    {
        private readonly UserAdministrationDbContext _dbContext;

        public GetPendingUsersDataRequest(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedList<UserDetailsResponse>> GetAsync((int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var query = (
                from dealer in _dbContext.Set<Dealer>()
                join user in _dbContext.Set<User>() on dealer.Id equals user.Id
                where dealer.Status == DealerStatus.Pending
                select user).AsNoTracking();

            var count = query.Count();

            var users = await query
                .OrderBy(users => users.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            IEnumerable<UserDetailsResponse> userDetailsResponses = users
                .Select(user => new UserDetailsResponse(
                    user.Id.Value,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Roles.Select(x => new RoleResponse(x.Id.Value, x.Description)).ToList()));

            return new PagedList<UserDetailsResponse>(userDetailsResponses, count, request.Page, request.ItemsPerPage);
        }
    }
}
