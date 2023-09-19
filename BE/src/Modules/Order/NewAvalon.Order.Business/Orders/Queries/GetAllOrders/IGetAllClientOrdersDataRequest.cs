using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using System;

namespace NewAvalon.Order.Business.Orders.Queries.GetAllOrders
{
    public interface IGetAllClientOrdersDataRequest : IDataRequest<(Guid OwnerId, int Page, int ItemsPerPage), PagedList<OrderDetailsResponse>>
    {
    }
}
