using NewAvalon.Messaging.Contracts.Products;
using System;

namespace NewAvalon.Catalog.Business.Contracts.Products
{
    internal sealed class ProductImageChangedEvent : IProductImageChangedEvent
    {
        public Guid OldImageId { get; set; }
    }
}
