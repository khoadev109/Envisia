using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Client : BaseEntity
    {
        public string? Name { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public IList<User> Users { get; set; } = new List<User>();

        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public IList<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();
    }
}
