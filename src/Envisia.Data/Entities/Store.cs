using Envisia.Data.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Envisia.Data.Entities
{
    public class Store : BaseEntity
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

        [Column(TypeName = "decimal(12, 9)")]
        public decimal? Longitude { get; set; }

        [Column(TypeName = "decimal(12, 9)")]
        public decimal? Latitude { get; set; }

        public int? LFA { get; set; }

        public int? CounterQty { get; set; }

        public string? DcName { get; set; } = string.Empty;

        public string? FacebookUrl { get; set; } = string.Empty;

        public int FormulaId { get; set; }

        public Formula Formula { get; set; }

        public int? ContactOwnerId { get; set; }

        public Contact ContactOwner { get; set; }

        public int? ContactManagerId { get; set; }

        public Contact ContactManager { get; set; }

        public int? LogoId { get; set; }

        public Logo Logo { get; set; }

        public IList<StoreFeature> StoreFeatures { get; set; } = new List<StoreFeature>();

        public IList<StoreServiceArea> StoreServiceAreas { get; set; } = new List<StoreServiceArea>();

        public IList<News> NewsList { get; set; } = new List<News>();
    }
}
