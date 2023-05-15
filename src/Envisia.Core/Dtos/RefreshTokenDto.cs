namespace Envisia.Application.Dtos
{
    public class RefreshTokenDto : BaseDto
    {
        public string Token { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Expires { get; set; }
    }
}
