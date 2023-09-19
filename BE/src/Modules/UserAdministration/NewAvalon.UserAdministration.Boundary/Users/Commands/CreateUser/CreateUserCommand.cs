using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string Username,
        string Email,
        string Password,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Address,
        int Roles) : ICommand<EntityCreatedResponse>;
}
