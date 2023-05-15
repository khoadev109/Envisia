using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Subscription : BaseEntity
    {
        public string Name { get; set; }

        public IList<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();
    }
}
