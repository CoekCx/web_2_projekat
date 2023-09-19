using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetPendingUsers
{
    public sealed record GetPendingUsersQuery(int Page, int ItemsPerPage) : IQuery<PagedList<UserDetailsResponse>>;
}
