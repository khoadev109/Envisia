using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class StoreFeature : BaseEntity
    {
        public string Feature { get; set; }

        public IList<Store> Stores { get; set; } = new List<Store>();
    }
}
