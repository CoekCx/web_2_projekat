using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetAllDealerUsers;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetAllDealerUsers
{
    public interface IGetAllDealerUsersDataRequest : IDataRequest<(int Page, int ItemsPerPage), PagedList<DealerUserDetailsResponse>>
    {
    }
}
