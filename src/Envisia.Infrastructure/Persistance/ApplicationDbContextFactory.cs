using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Envisia.Infrastructure.Persistance
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer("Server=localhost;Database=Envisia;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //DOTIN
            optionsBuilder.UseSqlServer("Server=.;Database=Envisia;User ID=sa;Password=admin@123;MultipleActiveResultSets=true;TrustServerCertificate=True");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
