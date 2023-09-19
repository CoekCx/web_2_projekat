using System.Linq;
using System.Security.Claims;

namespace NewAvalon.Authorization.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserIdentityId(this ClaimsPrincipal user) => user.Claims
            .SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)
            ?.Value;
    }
}
