using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class AreaPolygon : BaseEntity
    {
        public string? Code { get; set; } = string.Empty;

        public string? CountryAlpha2 { get; set; } = string.Empty;

        public string PolygonData { get; set; } = string.Empty;

        public IList<StoreServiceArea> StoreServiceAreas { get; set; } = new List<StoreServiceArea>();
    }
}
