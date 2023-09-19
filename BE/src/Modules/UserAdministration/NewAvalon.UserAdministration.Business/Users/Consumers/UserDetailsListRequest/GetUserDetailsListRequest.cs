using NewAvalon.UserAdministration.Domain.EntityIdentifiers;

namespace NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsListRequest
{
    public sealed record GetUserDetailsListRequest(UserId[] UserIds);
}
