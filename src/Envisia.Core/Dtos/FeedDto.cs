using Envisia.Data.Entities;

namespace Envisia.Application.Dtos
{
    public class FeedDto : BaseDto
    {
        public DateTime LastModifiedDate { get; set; }

        public string SourceUrl { get; set; }

        public IList<NewsDto> NewsList { get; set; } = new List<NewsDto>();
    }
}
