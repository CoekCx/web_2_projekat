using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using System;

namespace NewAvalon.Order.Business.Orders.Queries.GetAllOrders
{
    public interface IGetAllDealerUserOrdersDataRequest : IDataRequest<(Guid DealerId, int Page, int ItemsPerPage), PagedList<OrderDetailsResponse>>
    {
    }
}
