using System;

namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IGetCatalogProductResponse
    {
        Guid Id { get; set; }

        string Name { get; set; }

        decimal Price { get; set; }

        decimal Quantity { get; set; }

        string Description { get; set; }

        Guid CreatorId { get; set; }
    }
}
