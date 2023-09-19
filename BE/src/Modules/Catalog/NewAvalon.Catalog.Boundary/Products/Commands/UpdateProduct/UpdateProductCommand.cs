using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid ProductId,
        Guid CreatorId,
        string Name,
        decimal Price,
        decimal Capacity,
        string Description) : ICommand<Unit>;
}
