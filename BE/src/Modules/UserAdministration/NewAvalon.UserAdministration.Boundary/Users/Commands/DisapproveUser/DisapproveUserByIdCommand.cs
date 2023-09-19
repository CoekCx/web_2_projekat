using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.DisapproveUser
{
    public sealed record DisapproveUserByIdCommand(Guid UserId) : ICommand<Unit>;
}
