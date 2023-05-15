using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Infrastructure.Authorization;
using Envisia.Library;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    [HasPermission(Permission.AdminOrUser)]
    public class StoreController : EnvisiaControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService StoreService)
        {
            _storeService = StoreService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResult<IEnumerable<StoreDto>> serviceResult = await _storeService.GetAllAsync();

            if (serviceResult.Success)
            {
                return Ok(serviceResult.Result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
