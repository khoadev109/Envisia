using Envisia.Application.Interfaces.Authorization;
using Envisia.Application.Interfaces.Background;
using Envisia.Data.Interfaces;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Infrastructure.Authorization;
using Envisia.Infrastructure.Background;
using Envisia.Infrastructure.Persistance;
using Envisia.Infrastructure.Persistance.Repository;
using Hangfire;
using Hangfire.MemoryStorage;
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
            services.AddScoped<INewsRepository, NewsRepository>();

            return services;
        }

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IFeedResourceService>(sp =>
            {
                using var scope = sp.CreateScope();
                
                ApplicationDbContext dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                IConfiguration configuration = scope.ServiceProvider.GetService<IConfiguration>();
                IHttpClientFactory httpClientFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();

                return new FeedResourceService(httpClientFactory, configuration, dbContext);
            });

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

        public static void AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config => config.UseMemoryStorage());

            _ = services.AddHangfireServer(options => new BackgroundJobServerOptions { WorkerCount = Environment.ProcessorCount * configuration.GetSection("FeedHangfire:DegreeOfParallelism").Get<int>() });
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAppDbContext();
            services.AddRepositories();
            services.AddUnitOfWorks();
            services.AddServices();
            services.AddHangFire(configuration);

            return services;
        }
    }
}
