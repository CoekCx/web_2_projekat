using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using System;

namespace NewAvalon.Order.Boundary.Orders.Queries.GetShippingOrders
{
    public sealed record GetShippingOrdersQuery(Guid UserId, int Page, int ItemsPerPage) : IQuery<PagedList<OrderDetailsResponse>>;
}
