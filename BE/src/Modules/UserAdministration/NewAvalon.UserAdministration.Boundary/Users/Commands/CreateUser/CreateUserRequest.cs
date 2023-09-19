using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser
{
    public sealed record CreateUserRequest(
        string Username,
        string Email,
        string Password,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Address,
        int Roles);
}
