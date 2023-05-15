using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Application.Services;
using Envisia.Infrastructure.Authorization;
using Envisia.Library;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    [HasPermission(Permission.AdminOrUser)]
    public class NewsController : EnvisiaControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResult<IEnumerable<NewsDto>> serviceResult = await _newsService.GetAllAsync();

            if (serviceResult.Success)
            {
                return Ok(serviceResult.Result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
