using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Market : BaseEntity
    {
        public string Name { get; set; }

        public IList<Organisation> Organisations { get; set; } = new List<Organisation>();

        public IList<News> NewsList { get; set; } = new List<News>();

        public IList<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();
    }
}
