using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Orders
{
    public interface IOrderDeletedEvent : IEvent
    {
        public DeletedProductDetails[] Products { get; set; }
    }

    public class DeletedProductDetails
    {
        public Guid CatalogProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}
