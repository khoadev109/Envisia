namespace Envisia.Application.Dtos
{
    public class LogoDto : BaseDto
    {
        public byte[] Picture { get; set; }

        public string? Description { get; set; } = string.Empty;
    }
}
