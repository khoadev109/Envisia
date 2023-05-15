using Envisia.Application.Interfaces.Authorization;
using Envisia.Data.Interfaces;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Infrastructure.Authorization;
using Envisia.Infrastructure.Persistance;
using Envisia.Infrastructure.Persistance.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Envisia.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>()!;
            var connectionString = configuration.GetConnectionString("AppServer");

            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IFormulaRepository, FormulaRepository>();

            return services;
        }

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddAuthorizations(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAppDbContext();
            services.AddRepositories();
            services.AddUnitOfWorks();

            return services;
        }
    }
}
