using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using System;

namespace NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders
{
    public sealed record GetAllOrdersQuery(Guid UserId, int Page, int ItemsPerPage) : IQuery<PagedList<OrderDetailsResponse>>;
}
