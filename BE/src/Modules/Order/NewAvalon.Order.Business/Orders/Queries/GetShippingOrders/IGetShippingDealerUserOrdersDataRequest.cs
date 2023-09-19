using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using System;

namespace NewAvalon.Order.Business.Orders.Queries.GetShippingOrders
{
    public interface IGetShippingDealerUserOrdersDataRequest : IDataRequest<(Guid DealerId, int Page, int ItemsPerPage), PagedList<OrderDetailsResponse>>
    {
    }
}
