using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser
{
    public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserDetailsResponse>;
}
