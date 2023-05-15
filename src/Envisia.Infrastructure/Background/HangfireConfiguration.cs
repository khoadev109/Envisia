using Envisia.Application.Interfaces.Background;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Envisia.Infrastructure.Background
{
    public static class HangfireConfiguration
    {
        public static void UseHangFire(this IApplicationBuilder app, IConfiguration configuration)
        {
            var dashboardOptions = new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } };

            app.UseHangfireDashboard(configuration.GetSection("FeedHangfire:Dashboard").Get<string>(), dashboardOptions);

            UseCronJobs(app, configuration);
        }

        private static void UseCronJobs(IApplicationBuilder app, IConfiguration configuration)
        {
            var feedSyncingEnabled = configuration.GetSection("FeedHangfire:Enable").Get<bool>();
            if (!feedSyncingEnabled)
            {
                return;
            }

            var feedService = app.ApplicationServices.GetService<IFeedResourceService>() ?? throw new NotFoundException("Feed service does not exist.");

            Task.Run(async () =>
            {
                await feedService.StartSyncingAsync();
            });

            //RecurringJob.AddOrUpdate("collections", () => feedService.GetFeedResourcesAsync(), configuration.GetSection("FeedHangfire:CronExpression").Get<string>(), new RecurringJobOptions { TimeZone = TimeZoneInfo.Local });
        }
    }
}
