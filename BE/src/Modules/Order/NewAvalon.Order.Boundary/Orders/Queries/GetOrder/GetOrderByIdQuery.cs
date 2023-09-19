using NewAvalon.Abstractions.Messaging;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using System;

namespace NewAvalon.Order.Boundary.Orders.Queries.GetOrder
{
    public sealed record GetOrderByIdQuery(Guid OrderId, Guid UserId) : IQuery<OrderDetailsResponse>;
}
