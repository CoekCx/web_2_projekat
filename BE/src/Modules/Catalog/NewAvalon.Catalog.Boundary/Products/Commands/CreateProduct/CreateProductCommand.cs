using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        Guid CreatorId,
        string Name,
        decimal Price,
        decimal Capacity,
        string Description) : ICommand<EntityCreatedResponse>;
}
