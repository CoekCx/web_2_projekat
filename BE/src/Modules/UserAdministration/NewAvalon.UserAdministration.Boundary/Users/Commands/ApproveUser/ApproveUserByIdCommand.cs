using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.ApproveUser
{
    public sealed record ApproveUserByIdCommand(Guid UserId) : ICommand<Unit>;
}
