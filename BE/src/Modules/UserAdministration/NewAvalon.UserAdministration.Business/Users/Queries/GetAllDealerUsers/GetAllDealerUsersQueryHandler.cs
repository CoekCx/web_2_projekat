using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetAllDealerUsers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetAllDealerUsers
{
    internal sealed class GetAllDealerUsersQueryHandler : IQueryHandler<GetAllDealerUsersQuery, PagedList<DealerUserDetailsResponse>>
    {
        private readonly IGetAllDealerUsersDataRequest _getAllDealerUsersDataRequest;

        public GetAllDealerUsersQueryHandler(IGetAllDealerUsersDataRequest getAllDealerUsersDataRequest) =>
            _getAllDealerUsersDataRequest = getAllDealerUsersDataRequest;

        public async Task<PagedList<DealerUserDetailsResponse>> Handle(
            GetAllDealerUsersQuery request,
            CancellationToken cancellationToken) =>
            await _getAllDealerUsersDataRequest.GetAsync((request.Page, request.ItemsPerPage), cancellationToken);
    }
}
