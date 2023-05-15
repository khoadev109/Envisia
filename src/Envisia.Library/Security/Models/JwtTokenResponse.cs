namespace Envisia.Library.Security.Models
{
    public class JwtTokenResponse
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public string? UserIdentifier { get; set; }

        public string? TenantIdentifier { get; set; }
    }
}
