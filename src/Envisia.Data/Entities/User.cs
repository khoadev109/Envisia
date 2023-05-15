using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? MfaToken { get; set; } = string.Empty;

        public int? Status { get; set; }

        public DateTime? DateActive { get; set; } = null;

        public DateTime? DateDisabled { get; set; } = null;

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
