using MassTransit;
using NewAvalon.Messaging.Contracts.Permissions;
using NewAvalon.UserAdministration.Business.Contracts.Permissions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Permissions.Consumers
{
    public sealed class GetPermissionsRequestConsumer : IConsumer<IGetPermissionsRequest>
    {
        private readonly IGetPermissionsDataRequest _getPermissionsDataRequest;

        public GetPermissionsRequestConsumer(IGetPermissionsDataRequest getPermissionsDataRequest) => _getPermissionsDataRequest = getPermissionsDataRequest;

        public async Task Consume(ConsumeContext<IGetPermissionsRequest> context)
        {
            var request = new GetPermissionsRequest(new UserId(context.Message.UserId));

            string[] permissionNames = await _getPermissionsDataRequest.GetAsync(request, context.CancellationToken);

            var response = new GetPermissionsResponse
            {
                PermissionNames = permissionNames
            };

            await context.RespondAsync<IGetPermissionsResponse>(response);
        }
    }
}
