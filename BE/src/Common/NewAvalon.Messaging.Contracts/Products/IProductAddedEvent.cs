using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IProductAddedEvent : IEvent
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}
