using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }

        public int? LogoId { get; set; }

        public Logo Logo { get; set; }

        public IList<Market> Markets { get; set; } = new List<Market>();

        public IList<News> NewsList { get; set; } = new List<News>();

        public IList<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
