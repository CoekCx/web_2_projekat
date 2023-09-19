using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Authorization.Services
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(Permissions permission, CancellationToken cancellationToken = default, bool valueIfHttpContextNull = false);
    }
}
