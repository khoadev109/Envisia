namespace Envisia.Application.Dtos
{
    public class NewsDto : BaseDto
    {
        public DateTime DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; } = null;

        public string Subject { get; set; }

        public string Content { get; set; }

        public string? SourceUrl { get; set; } = string.Empty;

        public int? FeedId { get; set; }

        public int? OrganisationId { get; set; }

        public int? MarketId { get; set; }

        public int? StoreId { get; set; }

        public int? FormulaId { get; set; }

        public StoreDto? Store { get; set; }

        public FormulaDto? Formula { get; set;}
    }
}
