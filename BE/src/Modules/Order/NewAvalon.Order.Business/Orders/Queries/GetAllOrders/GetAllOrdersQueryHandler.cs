using MassTransit;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Authorization;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Business.Contracts.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Queries.GetAllOrders
{
    internal sealed class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, PagedList<OrderDetailsResponse>>
    {
        private readonly IRequestClient<IUserDetailsRequest> _userDetailsRequestClient;
        private readonly IGetAllSuperAdminOrdersDataRequest _getAllSuperAdminOrdersDataRequest;
        private readonly IGetAllDealerUserOrdersDataRequest _getAllDealerUserOrdersDataRequest;
        private readonly IGetAllClientOrdersDataRequest _getAllClientOrdersDataRequest;

        public GetAllOrdersQueryHandler(
            IRequestClient<IUserDetailsRequest> userDetailsRequestClient,
            IGetAllSuperAdminOrdersDataRequest getAllSuperAdminOrdersDataRequest,
            IGetAllDealerUserOrdersDataRequest getAllDealerUserOrdersDataRequest,
            IGetAllClientOrdersDataRequest getAllClientOrdersDataRequest)
        {
            _userDetailsRequestClient = userDetailsRequestClient;
            _getAllSuperAdminOrdersDataRequest = getAllSuperAdminOrdersDataRequest;
            _getAllDealerUserOrdersDataRequest = getAllDealerUserOrdersDataRequest;
            _getAllClientOrdersDataRequest = getAllClientOrdersDataRequest;
        }

        public async Task<PagedList<OrderDetailsResponse>> Handle(
            GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var userDetailsRequest = new UserDetailsRequest()
            {
                Id = request.UserId
            };

            var userDetailsResponse = (
                    await _userDetailsRequestClient.GetResponse<IUserDetailsResponse>(userDetailsRequest, cancellationToken))
                .Message;

            if (userDetailsResponse.Roles.Contains(RoleNames.SuperAdmin))
            {
                return await _getAllSuperAdminOrdersDataRequest.GetAsync((request.Page, request.ItemsPerPage), cancellationToken);
            }

            if (userDetailsResponse.Roles.Contains(RoleNames.DealerUser))
            {
                return await _getAllDealerUserOrdersDataRequest.GetAsync((request.UserId, request.Page, request.ItemsPerPage), cancellationToken);
            }

            if (userDetailsResponse.Roles.Contains(RoleNames.Client))
            {
                return await _getAllClientOrdersDataRequest.GetAsync((request.UserId, request.Page, request.ItemsPerPage), cancellationToken);
            }

            return new PagedList<OrderDetailsResponse>(new List<OrderDetailsResponse>(), 0, request.Page, request.ItemsPerPage);
        }
    }
}
