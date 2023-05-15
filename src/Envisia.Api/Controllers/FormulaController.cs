using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Infrastructure.Authorization;
using Envisia.Library;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    [HasPermission(Permission.AdminOrUser)]
    public class FormulaController : EnvisiaControllerBase
    {
        private readonly IFormulaService _formulaService;

        public FormulaController(IFormulaService formulaService)
        {
            _formulaService = formulaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResult<IEnumerable<FormulaDto>> serviceResult = await _formulaService.GetAllAsync();

            if (serviceResult.Success)
            {
                return Ok(serviceResult.Result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
