using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Logo : BaseEntity
    {
        public byte[] Picture { get; set; }

        public string? Description { get; set; } = string.Empty;

        public IList<Organisation> Organisations { get; set; } = new List<Organisation>();

        public IList<Formula> Formulas { get; set; } = new List<Formula>();

        public IList<Store> Stores { get; set; } = new List<Store>();
    }
}
