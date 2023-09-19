using NewAvalon.UserAdministration.Boundary.Contracts.Users;
using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser
{
    public sealed record UserDetailsResponse(Guid Id, string FirstName, string LastName, string Email, List<RoleResponse> Roles);
}
