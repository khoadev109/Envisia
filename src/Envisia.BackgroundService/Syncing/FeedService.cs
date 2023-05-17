using Envisia.BackgroundService.Data;
using Envisia.BackgroundService.RemoteResources;
using Envisia.Data.Entities;
using Envisia.Library.Extensions;
using Microsoft.Net.Http.Headers;
using System.Web;
using System.Xml.Serialization;

namespace Envisia.BackgroundService.Syncing
{
    public class FeedService : IFeedService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FeedService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task StartSyncing()
        {
            try
            {
                List<FeedModel> feeds = await FetchFeedSourceAsync();

                await ClearFeedsAndNewsInDb();

                await SaveFeedToDbAsync(feeds);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<List<FeedModel>> FetchFeedSourceAsync()
        {
            var feedModels = new List<FeedModel>();

            var feedUrl = _configuration.GetSection("Feed:SourceUrl").Get<string>();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, feedUrl)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/xml" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                XmlSerializer serializer = new(typeof(SiteMapIndex));

                SiteMapIndex feedSource = (SiteMapIndex)serializer.Deserialize(contentStream);

                foreach (var siteMap in feedSource.SiteMaps)
                {
                    var feedModel = new FeedModel
                    {
                        LastModified = siteMap.LastModified,
                        Url = siteMap.Location
                    };

                    var queryString = "?" + siteMap.Location.Split('?')[1];

                    var queryStringNameValues = HttpUtility.ParseQueryString(queryString);

                    var page = int.Parse(queryStringNameValues["currentPage"]);

                    if (page > 5)
                    {
                        return feedModels;
                    }

                    IEnumerable<FeedUrlModel> childModelUrls = await FetchFeedSourceUrlsAsync(siteMap.Location);

                    feedModel.Urls.AddRange(childModelUrls);

                    feedModels.Add(feedModel);
                }
            }

            return feedModels;
        }

        private async Task<IEnumerable<FeedUrlModel>> FetchFeedSourceUrlsAsync(string siteMapUrl)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, siteMapUrl)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/xml" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                XmlSerializer serializer = new(typeof(UrlSet));

                UrlSet feedSourceUrls = (UrlSet)serializer.Deserialize(contentStream);

                IEnumerable<FeedUrlModel> urlModels = ConvertToFeedSourceUrlsToFeedUrlModel(feedSourceUrls.Urls);

                return urlModels;
            }

            return Enumerable.Empty<FeedUrlModel>();
        }

        private List<FeedUrlModel> ConvertToFeedSourceUrlsToFeedUrlModel(IEnumerable<Url> urls)
        {
            var feedModelUrls = new List<FeedUrlModel>();

            foreach (Url url in urls)
            {
                var model = new FeedUrlModel
                {
                    LastModified = url.LastModified,
                    Url = url.Location
                };

                feedModelUrls.Add(model);
            }

            return feedModelUrls;
        }

        private async Task SaveFeedToDbAsync(List<FeedModel> feedModels)
        {
            using var dbContext = new ApplicationDbContext();

            foreach (FeedModel feedModel in feedModels)
            {
                var feed = new Feed
                {
                    LastModifiedDate = feedModel.LastModified,
                    SourceUrl = feedModel.Url,
                    NewsList = ConvertFeedUrlModelsToNewsList(feedModel.Urls)
                };

                await dbContext.AddAsync(feed);
            }

            await dbContext.SaveChangesAsync();
        }

        private List<News> ConvertFeedUrlModelsToNewsList(List<FeedUrlModel> feedUrlModels)
        {
            var newsList = new List<News>();

            var storeId = new Random().Next(1, 3);
            var formulaId = new Random().Next(1, 4);

            for (int i = 0; i < feedUrlModels.Count; i++)
            {
                FeedUrlModel feedUrlModel = feedUrlModels[i];

                var splitBySplash = feedUrlModel.Url.Split('/');

                var titleQueryString = splitBySplash[^1];

                var news = new News
                {
                    DateTimeFrom = feedUrlModel.LastModified,
                    SourceUrl = feedUrlModel.Url,
                    Subject = titleQueryString.GetTextFromQueryString(),
                    StoreId = i < feedUrlModels.Count / 2 - 1 ? storeId : null,
                    FormulaId = i >= feedUrlModels.Count / 2 ? formulaId : null,
                    CreatedBy = "TOAA"
                };

                newsList.Add(news);
            }

            return newsList;
        }

        private async Task ClearFeedsAndNewsInDb()
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Feeds.RemoveRange(dbContext.Feeds);

            dbContext.News.RemoveRange(dbContext.News);

            await dbContext.SaveChangesAsync();
        }
    }
}
