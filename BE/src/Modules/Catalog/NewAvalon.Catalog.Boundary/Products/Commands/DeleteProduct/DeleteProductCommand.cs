using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(
        Guid ProductId,
        Guid CreatorId) : ICommand<Unit>;
}
