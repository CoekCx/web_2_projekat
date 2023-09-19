using Microsoft.AspNetCore.Authorization;
using System;

namespace NewAvalon.Authorization.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission)
            : base(permission.ToString())
        {
        }
    }
}
