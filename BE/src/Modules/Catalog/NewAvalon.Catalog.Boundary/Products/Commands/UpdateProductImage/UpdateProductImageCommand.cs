using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProductImage
{
    public sealed record UpdateProductImageCommand(Guid ProductId, Guid UserId, Guid? ImageId) : ICommand<Unit>;
}
