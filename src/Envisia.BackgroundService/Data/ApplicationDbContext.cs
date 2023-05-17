using Envisia.Data.Entities;
using Envisia.Library.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Envisia.BackgroundService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Feed> Feeds => Set<Feed>();

        public DbSet<News> News => Set<News>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = AppSettingsHelper.GetConfiguration().GetConnectionString("AppServer");

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
