using NewAvalon.Messaging.Contracts.Products;
using System;

namespace NewAvalon.Order.Business.Contracts.Products
{
    internal sealed class ProductAddedEvent : IProductAddedEvent
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}
