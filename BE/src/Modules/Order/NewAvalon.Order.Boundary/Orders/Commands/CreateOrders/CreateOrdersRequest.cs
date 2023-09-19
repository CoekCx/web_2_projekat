using System;
using System.Collections.Generic;

namespace NewAvalon.Order.Boundary.Orders.Commands.CreateOrders
{
    public sealed record CreateOrdersRequest(List<AddProductRequest> Products, string Comment, string DeliveryAddress);

    public sealed record AddProductRequest(Guid Id, decimal Quantity);
}
