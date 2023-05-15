using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Formula : BaseEntity
    {
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public Organisation Organisation { get; set; }

        public int? LogoId { get; set; }

        public Logo Logo { get; set; }

        public IList<FormulaStat> FormulaStats { get; set; } = new List<FormulaStat>();

        public IList<News> NewsList { get; set; } = new List<News>();

        public IList<Contact> Contacts { get; set; } = new List<Contact>();

        public IList<Store> Stores { get; set; } = new List<Store>();
    }
}
