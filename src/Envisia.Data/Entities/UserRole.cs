using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class UserRole : BaseEntity
    {
        public int? ClientId { get; set; }

        public Client Client { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }
    }
}
