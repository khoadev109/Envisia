using System.Xml.Serialization;

namespace Envisia.Infrastructure.Background.RemoteResources
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class UrlSet
    {
        public UrlSet()
        {
            Urls = new List<Url>();
        }

        [XmlElement("url")]
        public List<Url> Urls { get; set; }
    }

    public class Url
    {
        [XmlElement("lastmod")]
        public DateTime LastModified { get; set; }

        [XmlElement("loc")]
        public string Location { get; set; }
    }
}
