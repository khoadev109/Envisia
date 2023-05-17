using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Infrastructure.Authorization;
using Envisia.Library;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    [HasPermission(Permission.AdminOrUser)]
    public class FeedNewsController : EnvisiaControllerBase
    {
        private readonly IFeedNewsService _feedNewsService;

        public FeedNewsController(IFeedNewsService feedNewsService)
        {
            _feedNewsService = feedNewsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResult<IEnumerable<FeedDto>> serviceResult = await _feedNewsService.GetAllFeedsAsync();

            if (serviceResult.Success)
            {
                return Ok(serviceResult.Result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
