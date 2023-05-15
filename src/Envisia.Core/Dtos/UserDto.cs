namespace Envisia.Application.Dtos
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? MfaToken { get; set; } = string.Empty;

        public int? Status { get; set; }

        public DateTime? DateActive { get; set; } = null;

        public DateTime? DateDisabled { get; set; } = null;

        public int ClientId { get; set; }
    }
}
