namespace Envisia.Application.Dtos
{
    public class StoreDto : BaseDto
    {
        public string StoreName { get; set; }

        public bool Franchise { get; set; }

        public string CountryAlpha2 { get; set; }

        public string? Address { get; set; } = string.Empty;

        public string? HouseNumber { get; set; } = string.Empty;

        public string? Zip { get; set; } = string.Empty;

        public string? City { get; set; } = string.Empty;

        public string? Province { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public int? LFA { get; set; }

        public int? CounterQty { get; set; }

        public string? DcName { get; set; } = string.Empty;

        public string? FacebookUrl { get; set; } = string.Empty;

        public int FormulaId { get; set; }

        public int? ContactOwnerId { get; set; }

        public int? ContactManagerId { get; set; }

        public int? LogoId { get; set; }

        public LogoDto Logo { get; set; }
    }
}
