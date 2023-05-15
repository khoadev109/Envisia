using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface IOrganisationService
    {
        Task<ServiceResult<IEnumerable<OrganisationDto>>> GetAllAsync();
    }
}
