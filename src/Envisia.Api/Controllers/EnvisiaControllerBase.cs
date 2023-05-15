using Envisia.Library.Security.Models;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvisiaControllerBase : ControllerBase
    {
        public string CurrentUserId => User.Claims.First(x => x.Type == ClaimConstants.UserId)?.Value ?? string.Empty;

        public string CurrentUserName => User.Claims.First(x => x.Type == ClaimConstants.UserName)?.Value ?? string.Empty;
    }
}
