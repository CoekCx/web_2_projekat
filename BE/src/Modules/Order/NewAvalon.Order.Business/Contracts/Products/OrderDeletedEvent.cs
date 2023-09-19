using NewAvalon.Messaging.Contracts.Orders;

namespace NewAvalon.Order.Business.Contracts.Products
{
    internal class OrderDeletedEvent : IOrderDeletedEvent
    {
        public DeletedProductDetails[] Products { get; set; }
    }
}
