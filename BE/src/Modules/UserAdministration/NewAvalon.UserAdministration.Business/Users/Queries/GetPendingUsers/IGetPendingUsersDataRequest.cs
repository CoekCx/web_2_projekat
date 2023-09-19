using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetPendingUsers
{
    public interface IGetPendingUsersDataRequest : IDataRequest<(int Page, int ItemsPerPage), PagedList<UserDetailsResponse>>
    {
    }
}
