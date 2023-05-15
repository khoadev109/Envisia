using Envisia.Data.Entities;
using Envisia.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Envisia.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AreaPolygon> AreaPolygons => Set<AreaPolygon>();

        public DbSet<AreaStats> AreaStats => Set<AreaStats>();

        public DbSet<Client> Clients => Set<Client>();

        public DbSet<ClientSubscription> ClientSubscriptions => Set<ClientSubscription>();

        public DbSet<Contact> Contacts => Set<Contact>();

        public DbSet<Formula> Formulas => Set<Formula>();

        public DbSet<FormulaStat> FormulaStats => Set<FormulaStat>();

        public DbSet<Market> Markets => Set<Market>();

        public DbSet<News> News => Set<News>();

        public DbSet<Organisation> Organisations => Set<Organisation>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Store> Stores => Set<Store>();

        public DbSet<StoreFeature> StoreFeatures => Set<StoreFeature>();

        public DbSet<StoreServiceArea> StoreServiceAreas => Set<StoreServiceArea>();

        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        public DbSet<User> Users => Set<User>();

        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public DbSet<Logo> Logos { get; set; }

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Feed> Feeds => Set<Feed>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                if (insertedEntry is IAuditableEntity auditableEntity)
                {
                    auditableEntity.CreatedDateTime = DateTime.Now;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry is IAuditableEntity auditableEntity)
                {
                    auditableEntity.ModifiedDateTime = DateTime.Now;
                }
            }

            var deletedEntries = ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Deleted)
                       .Select(x => x.Entity);

            foreach (var deletedEntry in deletedEntries)
            {
                if (deletedEntry is IAuditableEntity auditableEntity)
                {
                    auditableEntity.DeletedDateTime = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
