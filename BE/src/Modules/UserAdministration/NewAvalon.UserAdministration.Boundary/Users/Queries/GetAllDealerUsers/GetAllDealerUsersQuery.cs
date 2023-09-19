using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetAllDealerUsers
{
    public sealed record GetAllDealerUsersQuery(int Page, int ItemsPerPage) : IQuery<PagedList<DealerUserDetailsResponse>>;
}
