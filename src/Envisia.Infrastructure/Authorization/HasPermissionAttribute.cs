using Microsoft.AspNetCore.Authorization;

namespace Envisia.Infrastructure.Authorization
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString())
        {
        }
    }
}
