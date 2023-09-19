using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Storage.Boundary.Images.Commands.DeleteImage
{
    public record DeleteImageCommand(Guid ImageId) : ICommand<Unit>;
}
