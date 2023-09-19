using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser
{
    public sealed record UpdateUserRequest(
        string Username,
        string Password,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Address);
}
