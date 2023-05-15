using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Envisia.Infrastructure.Persistance
{
    public static class DataInitializer
    {
        public async static Task BootstrapDb(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var hostEnvironment = services.GetRequiredService<IHostEnvironment>();

            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

            if (hostEnvironment?.IsDevelopment() ?? false)
            {
                await ApplicationDataSeed.Run(context);
            }
        }
    }
}
