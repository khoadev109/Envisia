namespace Envisia.Library.Security.Models
{
    public class ForgotPasswordMessage
    {
        public string ResetToken { get; set; }
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
