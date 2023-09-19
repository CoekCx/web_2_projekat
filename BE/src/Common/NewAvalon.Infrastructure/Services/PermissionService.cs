using MassTransit;
using Microsoft.AspNetCore.Http;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Authorization;
using NewAvalon.Authorization.Extensions;
using NewAvalon.Authorization.Services;
using NewAvalon.Messaging.Contracts.Permissions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Infrastructure.Services
{
    internal sealed class PermissionService : IPermissionService, ITransient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestClient<IGetPermissionsRequest> _requestClient;

        public PermissionService(
            IHttpContextAccessor httpContextAccessor,
            IRequestClient<IGetPermissionsRequest> requestClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestClient = requestClient;
        }

        public async Task<bool> HasPermissionAsync(Permissions permission, CancellationToken cancellationToken = default, bool valueIfHttpContextNull = false)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return valueIfHttpContextNull;
            }

            if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.GetUserIdentityId(), out Guid userIdentityProviderId))
            {
                return false;
            }

            var request = new GetPermissionsRequest
            {
                UserId = userIdentityProviderId
            };

            IGetPermissionsResponse response =
                (await _requestClient.GetResponse<IGetPermissionsResponse>(request, cancellationToken)).Message;

            return response.PermissionNames.Contains(permission.ToString());
        }

        private sealed class GetPermissionsRequest : IGetPermissionsRequest
        {
            public Guid UserId { get; set; }
        }
    }
}