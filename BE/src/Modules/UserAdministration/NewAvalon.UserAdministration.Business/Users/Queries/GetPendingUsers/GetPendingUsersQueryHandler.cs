using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetPendingUsers;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetPendingUsers
{
    internal sealed class GetPendingUsersQueryHandler : IQueryHandler<GetPendingUsersQuery, PagedList<UserDetailsResponse>>
    {
        private readonly IGetPendingUsersDataRequest _getUnApprovedUsersDataRequest;

        public GetPendingUsersQueryHandler(IGetPendingUsersDataRequest getUnApprovedUsersDataRequest)
        {
            _getUnApprovedUsersDataRequest = getUnApprovedUsersDataRequest;
        }

        public async Task<PagedList<UserDetailsResponse>> Handle(GetPendingUsersQuery request, CancellationToken cancellationToken) =>
            await _getUnApprovedUsersDataRequest.GetAsync((request.Page, request.ItemsPerPage), cancellationToken);
    }
}
