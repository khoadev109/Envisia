using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class ClientSubscription : BaseEntity
    {
        public DateTime? DateStart { get; set; } = null;

        public DateTime? DateEnd { get; set; } = null;

        public string? Status { get; set; } = string.Empty;

        public int? ClientId { get; set; }

        public Client Client { get; set; }

        public int? SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }

        public int? MarketId { get; set; }

        public Market Market { get; set; }
    }
}
