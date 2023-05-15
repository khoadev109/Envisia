namespace Envisia.Library.Security.Models
{
    public record class LoginRequest(string UserName, string Password, IEnumerable<string>? Roles = null);
}
