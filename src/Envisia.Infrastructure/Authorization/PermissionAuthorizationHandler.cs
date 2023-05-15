using Envisia.Application.Interfaces.Authorization;
using Envisia.Library.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Envisia.Infrastructure.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimConstants.UserId)?.Value;

            if (!int.TryParse(userId, out int parseUserId))
            {
                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            if (requirement.Permission.ValidPermission(Permission.Admin))
            {
                bool isAdmin = await permissionService.CheckAsAdminAsync(parseUserId);
                if (isAdmin)
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Permission.ValidPermission(Permission.User))
            {
                bool isUser = await permissionService.CheckAsUserAsync(parseUserId);
                if (isUser)
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Permission.ValidPermission(Permission.AdminOrUser))
            {
                bool isAdminOrUser = await permissionService.CheckAsAdminOrUserAsync(parseUserId);
                if (isAdminOrUser)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
