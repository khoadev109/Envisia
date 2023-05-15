using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface IStoreService
    {
        Task<ServiceResult<IEnumerable<StoreDto>>> GetAllAsync();
    }
}
