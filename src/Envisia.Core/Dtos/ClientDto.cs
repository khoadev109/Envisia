namespace Envisia.Application.Dtos
{
    public class ClientDto : BaseDto
    {
        public string? Name { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
    }
}
