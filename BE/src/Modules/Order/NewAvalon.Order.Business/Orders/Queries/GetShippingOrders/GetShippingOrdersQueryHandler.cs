using MassTransit;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Authorization;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Boundary.Orders.Queries.GetShippingOrders;
using NewAvalon.Order.Business.Contracts.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Queries.GetShippingOrders
{
    internal sealed class GetShippingOrdersQueryHandler : IQueryHandler<GetShippingOrdersQuery, PagedList<OrderDetailsResponse>>
    {
        private readonly IRequestClient<IUserDetailsRequest> _userDetailsRequestClient;
        private readonly IGetShippingDealerUserOrdersDataRequest _getShippingDealerUserOrdersDataRequest;
        private readonly IGetShippingClientOrdersDataRequest _getShippingClientOrdersDataRequest;

        public GetShippingOrdersQueryHandler(
            IRequestClient<IUserDetailsRequest> userDetailsRequestClient,
            IGetShippingDealerUserOrdersDataRequest getShippingDealerUserOrdersDataRequest,
            IGetShippingClientOrdersDataRequest getShippingClientOrdersDataRequest)
        {
            _userDetailsRequestClient = userDetailsRequestClient;
            _getShippingDealerUserOrdersDataRequest = getShippingDealerUserOrdersDataRequest;
            _getShippingClientOrdersDataRequest = getShippingClientOrdersDataRequest;
        }

        public async Task<PagedList<OrderDetailsResponse>> Handle(
            GetShippingOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var userDetailsRequest = new UserDetailsRequest()
            {
                Id = request.UserId
            };

            var userDetailsResponse = (
                    await _userDetailsRequestClient.GetResponse<IUserDetailsResponse>(userDetailsRequest, cancellationToken))
                .Message;

            if (userDetailsResponse.Roles.Contains(RoleNames.DealerUser))
            {
                return await _getShippingDealerUserOrdersDataRequest.GetAsync((request.UserId, request.Page, request.ItemsPerPage), cancellationToken);
            }

            if (userDetailsResponse.Roles.Contains(RoleNames.Client))
            {
                return await _getShippingClientOrdersDataRequest.GetAsync((request.UserId, request.Page, request.ItemsPerPage), cancellationToken);
            }

            return new PagedList<OrderDetailsResponse>(new List<OrderDetailsResponse>(), 0, request.Page, request.ItemsPerPage);
        }
    }
}
