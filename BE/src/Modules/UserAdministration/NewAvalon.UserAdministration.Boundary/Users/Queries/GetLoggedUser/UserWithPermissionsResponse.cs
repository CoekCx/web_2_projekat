using NewAvalon.UserAdministration.Boundary.Contracts.Users;
using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetLoggedUser
{
    public sealed record UserWithPermissionsResponse(
        Guid Id,
        string Username,
        string FirstName,
        string LastName,
        string Email,
        string Address,
        int? Status,
        ProfileImageResponse ProfileImage,
        List<RoleResponse> Roles,
        List<PermissionResponse> Permissions);

    public sealed record ProfileImageResponse(
        Guid Id,
        string Url);
}
