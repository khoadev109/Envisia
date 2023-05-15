using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class AreaStats : BaseEntity
    {
        public string Name { get; set; }

        public int? StoreServiceAreaId { get; set; }

        public StoreServiceArea StoreServiceArea { get; set; }
    }
}
