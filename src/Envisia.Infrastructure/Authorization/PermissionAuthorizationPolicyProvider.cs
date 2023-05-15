﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Envisia.Infrastructure.Authorization
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public async override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            return new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(policyName)).Build();
        }
    }
}
