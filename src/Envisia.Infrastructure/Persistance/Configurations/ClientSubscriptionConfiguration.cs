using Envisia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Envisia.Infrastructure.Persistance.Configurations
{
    public class ClientSubscriptionConfiguration : IEntityTypeConfiguration<ClientSubscription>
    {
        public void Configure(EntityTypeBuilder<ClientSubscription> builder)
        {
            builder.HasKey(cs => new { cs.ClientId, cs.SubscriptionId });

            builder.HasOne<Client>(cs => cs.Client)
                    .WithMany(c => c.ClientSubscriptions)
                    .HasForeignKey(cs => cs.ClientId);


            builder.HasOne<Subscription>(cs => cs.Subscription)
                    .WithMany(s => s.ClientSubscriptions)
                    .HasForeignKey(cs => cs.SubscriptionId);
        }
    }
}
