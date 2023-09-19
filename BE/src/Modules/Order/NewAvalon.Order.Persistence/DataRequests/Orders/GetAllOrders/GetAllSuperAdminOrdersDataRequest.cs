using MassTransit;
using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Business.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Persistence.Contracts.Users;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.DataRequests.Orders.GetAllOrders
{
    internal sealed class GetAllSuperAdminOrdersDataRequest : IGetAllSuperAdminOrdersDataRequest
    {
        private readonly OrderDbContext _dbContext;

        private readonly IRequestClient<IUserDetailsListRequest> _userDetailsListRequest;

        public GetAllSuperAdminOrdersDataRequest(
            OrderDbContext dbContext,
            IRequestClient<IUserDetailsListRequest> userDetailsListRequest)
        {
            _dbContext = dbContext;
            _userDetailsListRequest = userDetailsListRequest;
        }

        public async Task<PagedList<OrderDetailsResponse>> GetAsync((int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var orders = await _dbContext.Set<Domain.Entities.Order>()
                .Include(order => order.Products)
                .OrderBy(order => order.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            var userDetailsListRequest = new UserDetailsListRequest()
            {
                UserIds = orders
                    .Select(x => x.OwnerId)
                    .Union(orders.Select(x => x.DealerId))
                    .ToArray()
            };

            var userDetailsListResponse = (
                    await _userDetailsListRequest.GetResponse<IUserDetailsListResponse>(
                        userDetailsListRequest,
                        cancellationToken))
                .Message;

            var response = orders.Select(order =>
            {
                var owner = userDetailsListResponse.Users.First(x => x.Id == order.OwnerId);
                var dealer = userDetailsListResponse.Users.First(x => x.Id == order.DealerId);

                return new OrderDetailsResponse(
                    order.Id.Value,
                    new OrderUserDetailsResponse(
                        owner.Id,
                        owner.FirstName,
                        owner.LastName,
                        owner.Username),
                    new OrderUserDetailsResponse(
                        dealer.Id,
                        dealer.FirstName,
                        dealer.LastName,
                        dealer.Username),
                    order.GetName(),
                    order.Comment,
                    order.DeliveryAddress,
                    (int)order.Status,
                    order.Products.Select(product => new ProductDetailsResponse(
                        product.Id.Value,
                        product.OrderId.Value,
                        product.CatalogProductId,
                        product.Quantity,
                        product.Price,
                        product.GetFullPrice(),
                        product.Name)).ToList(),
                    Domain.Entities.Order.GetDeliveryPrice(),
                    order.GetFullPrice(),
                    order.DeliveryOnUtc);
            });

            var count = await _dbContext.Set<Domain.Entities.Order>().CountAsync(cancellationToken: cancellationToken);

            return new PagedList<OrderDetailsResponse>(response, count, request.Page, request.ItemsPerPage);
        }
    }
}
