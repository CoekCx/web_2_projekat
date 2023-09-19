using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetLoggedUser
{
    public sealed record GetLoggedUserByIdQuery(Guid UserId) : IQuery<UserWithPermissionsResponse>;
}
