using Envisia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Envisia.Infrastructure.Persistance
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
            //optionsBuilder.UseSqlServer("Server=localhost;Database=Envisia;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //DOTIN
            optionsBuilder.UseSqlServer("Server=.;Database=Envisia;User ID=sa;Password=admin@123;MultipleActiveResultSets=true;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
