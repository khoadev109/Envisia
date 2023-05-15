namespace Envisia.BackgroundService.Syncing
{
    public class FeedModel
    {
        public DateTime LastModified { get; set; }

        public string Url { get; set; }

        public List<FeedUrlModel> Urls { get; set; } = new List<FeedUrlModel>();
    }

    public class FeedUrlModel
    {
        public DateTime LastModified { get; set; }

        public string Url { get; set; }
    }
}
