using System.ComponentModel.DataAnnotations;

namespace Envisia.Library.Security.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }
}
