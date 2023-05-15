using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class StoreServiceArea : BaseEntity
    {
        public string? StatisticCode { get; set; } = string.Empty;

        public int? PolygonId { get; set; }

        public AreaPolygon Polygon { get; set; }

        public int? StoreId { get; set; }

        public Store Store { get; set; }
    }
}
