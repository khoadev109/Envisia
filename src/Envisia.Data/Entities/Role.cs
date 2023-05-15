using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
