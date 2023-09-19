using MassTransit;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.UserAdministration.Business.Contracts.Users;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsListRequest
{
    public sealed class UserDetailsListRequestConsumer : IConsumer<IUserDetailsListRequest>
    {
        private readonly IGetUserDetailsListDataRequest _getUserDetailsListDataRequest;

        public UserDetailsListRequestConsumer(IGetUserDetailsListDataRequest getUserDetailsListDataRequest) =>
            _getUserDetailsListDataRequest = getUserDetailsListDataRequest;

        public async Task Consume(ConsumeContext<IUserDetailsListRequest> context)
        {
            UserId[] userIds = context.Message.UserIds.Select(userId => new UserId(userId)).ToArray();

            var userDetailsListRequest = new GetUserDetailsListRequest(userIds);

            List<IUserDetailsResponse> userDetails = await _getUserDetailsListDataRequest.GetAsync(userDetailsListRequest);

            var response = new UserDetailsListResponse
            {
                Users = userDetails.ToArray()
            };

            await context.RespondAsync<IUserDetailsListResponse>(response);
        }
    }
}
