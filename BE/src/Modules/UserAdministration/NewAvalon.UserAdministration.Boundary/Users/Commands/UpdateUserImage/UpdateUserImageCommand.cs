using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUserImage
{
    public sealed record UpdateUserImageCommand(Guid Id, Guid? ImageId) : ICommand<Unit>;
}
