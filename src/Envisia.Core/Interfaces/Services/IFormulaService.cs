using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface IFormulaService
    {
        Task<ServiceResult<IEnumerable<FormulaDto>>> GetAllAsync();
    }
}
