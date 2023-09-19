using Microsoft.EntityFrameworkCore;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsListRequest;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Persistence.Contracts.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.DataRequests.Users
{
    internal sealed class GetUserDetailsListDataRequest : IGetUserDetailsListDataRequest
    {
        private readonly UserAdministrationDbContext _dbContext;

        public GetUserDetailsListDataRequest(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<IUserDetailsResponse>> GetAsync(
            GetUserDetailsListRequest request,
            CancellationToken cancellationToken = default)
        {
            List<UserDetailsResponse> users = await _dbContext.Set<User>()
                .Where(user => request.UserIds.Contains(user.Id))
                .Select(user => new UserDetailsResponse
                {
                    Id = user.Id.Value,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = user.Roles.Select(role => role.Description).ToList(),
                })
                .ToListAsync(cancellationToken);

            return users.ConvertAll(userDetails => (IUserDetailsResponse)userDetails);
        }
    }
}