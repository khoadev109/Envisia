using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Library;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Cryptography;

namespace Envisia.Api.Controllers
{
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
