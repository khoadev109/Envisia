using Envisia.BackgroundService.Syncing;
using Envisia.Infrastructure.Persistance;
using Hangfire;

namespace Envisia.BackgroundService.Hangfire
{
    public static class HangfireConfiguration
    {
        public static void UseHangFire(this IApplicationBuilder app, IConfiguration configuration)
        {
            var dashboardOptions = new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } };

            app.UseHangfireDashboard(configuration.GetSection("Feed:Dashboard").Get<string>(), dashboardOptions);

            UseCronJobs(app, configuration);
        }

        private static void UseCronJobs(IApplicationBuilder app, IConfiguration configuration)
        {
            var feedSyncingEnabled = configuration.GetSection("Feed:Enable").Get<bool>();
            if (!feedSyncingEnabled)
            {
                return;
            }

            var feedService = app.ApplicationServices.GetService<IFeedService>() ?? throw new NotFoundException("Feed service does not exist.");

            using var dbContext = new ApplicationDbContext();

            if (!dbContext.Feeds.Any() || !dbContext.News.Any())
            {
                Task.Run(async () =>
                {
                    await feedService.StartSyncing();
                });
            }
            else
            {
                RecurringJob.AddOrUpdate("collections", () => feedService.StartSyncing(), configuration.GetSection("Feed:CronExpression").Get<string>(), new RecurringJobOptions { TimeZone = TimeZoneInfo.Local });
            }
        }
    }
}
