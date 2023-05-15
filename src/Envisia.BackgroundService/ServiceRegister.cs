using Envisia.BackgroundService.Syncing;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Envisia.BackgroundService
{
    public static class ServiceRegister
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config => config.UseMemoryStorage());

            _ = services.AddHangfireServer(options => new BackgroundJobServerOptions { WorkerCount = Environment.ProcessorCount * configuration.GetSection("Feed:DegreeOfParallelism").Get<int>() });

            services.AddHttpClient();

            services.AddSingleton<IFeedService, FeedService>();

            return services;
        }
    }
}
