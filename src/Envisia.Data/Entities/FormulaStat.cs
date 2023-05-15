using Envisia.Data.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Envisia.Data.Entities
{
    public class FormulaStat : BaseEntity
    {
        public string? Annum { get; set; } = string.Empty;

        [Column(TypeName = "decimal(12, 0)")]
        public decimal? Turnover { get; set; }

        public int? LFATotal { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal? MarketShareIRI { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal? MarketShareNielsen { get; set; }

        public int? Store { get; set; }

        public int? Franchise { get; set; }

        public int? FormulaId { get; set; }

        public Formula Formula { get; set; }
    }
}
