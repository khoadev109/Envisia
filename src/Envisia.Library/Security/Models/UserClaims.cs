namespace Envisia.Library.Security.Models
{
    public class UserClaims
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
