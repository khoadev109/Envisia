using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface IFeedNewsService
    {
        Task<ServiceResult<IEnumerable<NewsDto>>> GetAllNewsAsync();

        Task<ServiceResult<IEnumerable<FeedDto>>> GetAllFeedsAsync();
    }
}
