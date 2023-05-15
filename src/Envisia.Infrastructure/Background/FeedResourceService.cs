using Envisia.Application.Interfaces.Background;
using Envisia.Core.BackgroundModels;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Infrastructure.Background.RemoteResources;
using Envisia.Infrastructure.Persistance;
using Envisia.Library.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Xml.Serialization;

namespace Envisia.Infrastructure.Background
{
    public class FeedResourceService : IFeedResourceService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationDbContext _dbContext;

        public FeedResourceService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
        }

        public async Task<List<FeedModel>> StartSyncingAsync()
        {
            List<FeedModel> feeds = await FetchFeedSourceAsync();

            await SaveFeedsAndNewsAsync(feeds);

            return feeds;
        }

        private async Task<List<FeedModel>> FetchFeedSourceAsync()
        {
            var feedModels = new List<FeedModel>();

            var feedUrl = _configuration.GetSection("FeedHangfire:FeedSourceUrl").Get<string>();
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

                try
                {
                    XmlSerializer serializer = new(typeof(SiteMapIndex));

                    SiteMapIndex feedSource = (SiteMapIndex)serializer.Deserialize(contentStream);

                    foreach (var siteMap in feedSource.SiteMaps)
                    {
                        var feedModel = new FeedModel
                        {
                            LastModified = siteMap.LastModified,
                            Url = siteMap.Location
                        };

                        IEnumerable<FeedUrlModel> childModelUrls = await FetchFeedSourceUrlsAsync(siteMap.Location);

                        feedModel.Urls.AddRange(childModelUrls);

                        feedModels.Add(feedModel);
                    }
                }
                catch (Exception ex)
                {

                    throw;
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

        private async Task SaveFeedsAndNewsAsync(List<FeedModel> feedModels)
        {
            try
            {
                foreach (var feedModel in feedModels)
                {
                    var newsList = ConvertFeedUrlsToNewsList(feedModel.Urls);

                    var feed = new Feed
                    {
                        LastModifiedDate = feedModel.LastModified,
                        SourceUrl = feedModel.Url,
                        NewsList = newsList
                    };

                    await _dbContext.Feeds.AddAsync(feed);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private List<News> ConvertFeedUrlsToNewsList(IEnumerable<FeedUrlModel> urls)
        {
            var newsList = new List<News>();

            foreach (var url in urls)
            {
                var splitBySplash = url.Url.Split('/');
                var subject = splitBySplash[^1].GetTextFromQueryString();

                var news = new News
                {
                    DateTimeFrom = url.LastModified,
                    SourceUrl = url.Url,
                    Subject = subject
                };

                newsList.Add(news);
            }

            return newsList;
        }
    }
}
