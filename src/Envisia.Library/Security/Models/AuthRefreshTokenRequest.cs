namespace Envisia.Library.Security.Models
{
    public class AuthRefreshTokenRequest
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public IEnumerable<string>? Roles { get; set; }
    }
}
