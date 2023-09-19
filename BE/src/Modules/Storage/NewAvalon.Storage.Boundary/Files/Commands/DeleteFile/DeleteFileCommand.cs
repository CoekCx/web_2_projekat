using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Storage.Boundary.Files.Commands.DeleteFile
{
    public record DeleteFileCommand(Guid FileId) : ICommand<Unit>;
}
