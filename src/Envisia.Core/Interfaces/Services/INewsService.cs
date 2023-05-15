using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface INewsService
    {
        Task<ServiceResult<IEnumerable<NewsDto>>> GetAllAsync();
    }
}
