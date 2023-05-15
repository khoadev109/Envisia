using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<ServiceResult<IEnumerable<NewsDto>>> GetByOrganisationIdAsync(int organisationId);

        Task<ServiceResult<IEnumerable<NewsDto>>> GetByFormulaIdAsync(int formulaId);

        Task<ServiceResult<IEnumerable<NewsDto>>> GetByStoreIdAsync(int storeId);
    }
}
