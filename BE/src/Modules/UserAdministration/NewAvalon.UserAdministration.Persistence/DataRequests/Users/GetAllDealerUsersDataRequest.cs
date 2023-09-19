using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetAllDealerUsers;
using NewAvalon.UserAdministration.Business.Users.Queries.GetAllDealerUsers;
using NewAvalon.UserAdministration.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.DataRequests.Users
{
    internal sealed class GetAllDealerUsersDataRequest : IGetAllDealerUsersDataRequest
    {
        private readonly UserAdministrationDbContext _dbContext;

        public GetAllDealerUsersDataRequest(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedList<DealerUserDetailsResponse>> GetAsync((int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var query = (
                from dealer in _dbContext.Set<Dealer>()
                join user in _dbContext.Set<User>() on dealer.Id equals user.Id
                select new
                {
                    Id = user.Id.Value,
                    user.FirstName,
                    user.LastName,
                    user.CreatedOnUtc,
                    user.Email,
                    Status = (int)dealer.Status
                })
                .AsNoTracking();

            var count = query.Count();

            var users = await query
                .OrderBy(users => users.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            IEnumerable<DealerUserDetailsResponse> userDetailsResponses = users
                .Select(user => new DealerUserDetailsResponse(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Status));

            return new PagedList<DealerUserDetailsResponse>(userDetailsResponses, count, request.Page, request.ItemsPerPage);
        }
    }
}
