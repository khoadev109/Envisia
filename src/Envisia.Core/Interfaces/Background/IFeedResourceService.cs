using Envisia.Core.BackgroundModels;

namespace Envisia.Application.Interfaces.Background
{
    public interface IFeedResourceService
    {
        Task<List<FeedModel>> StartSyncingAsync();
    }
}
