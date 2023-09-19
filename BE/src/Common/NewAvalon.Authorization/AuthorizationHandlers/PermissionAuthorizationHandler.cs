using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Authorization.Extensions;
using NewAvalon.Authorization.Requirements;
using NewAvalon.Messaging.Contracts.Permissions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewAvalon.Authorization.AuthorizationHandlers
{
    /// <summary>
    /// Represents the permission authorization handler.
    /// </summary>
    public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

        /// <inheritdoc />
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!Enum.TryParse(requirement.PermissionName, false, out Permissions requiredPermission))
            {
                return;
            }

            Permissions[] permissions = await GetPermissionsAsync(context.User);

            if (permissions.Any(permission => permission == requiredPermission))
            {
                context.Succeed(requirement);
            }
        }

        private async Task<Permissions[]> GetPermissionsAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal is null)
            {
                return Array.Empty<Permissions>();
            }

            if (!Guid.TryParse(claimsPrincipal.GetUserIdentityId(), out Guid userIdentityId))
            {
                return Array.Empty<Permissions>();
            }

            using IServiceScope scope = _scopeFactory.CreateScope();

            IRequestClient<IGetPermissionsRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<IGetPermissionsRequest>>();

            var request = new GetPermissionsRequest
            {
                UserId = userIdentityId
            };

            Response<IGetPermissionsResponse> permissionNames = await requestClient.GetResponse<IGetPermissionsResponse>(request);

            return permissionNames.Message.PermissionNames.Select(Enum.Parse<Permissions>).ToArray();
        }

        private sealed class GetPermissionsRequest : IGetPermissionsRequest
        {
            public Guid UserId { get; set; }
        }
    }
}
