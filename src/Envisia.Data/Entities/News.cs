using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class News : BaseEntity
    {
        public DateTime DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; } = null;

        public string Subject { get; set; }

        public string? Content { get; set; } = string.Empty;

        public string SourceUrl { get; set; }

        public int? FeedId { get; set; }

        public Feed Feed { get; set; }

        public int? OrganisationId { get; set; }

        public Organisation Organisation { get; set; }

        public int? MarketId { get; set; }

        public Market Market { get; set; }

        public int? StoreId { get; set; }

        public Store Store { get; set; }

        public int? FormulaId { get; set; }

        public Formula Formula { get; set; }
    }
}
