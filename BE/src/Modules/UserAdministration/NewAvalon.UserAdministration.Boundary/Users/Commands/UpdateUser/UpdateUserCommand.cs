using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string Username,
        string Password,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Address) : ICommand<Unit>;
}
