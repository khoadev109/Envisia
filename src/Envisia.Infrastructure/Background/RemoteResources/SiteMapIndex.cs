using Envisia.Data.Entities;
using System.Xml.Serialization;

namespace Envisia.Infrastructure.Background.RemoteResources
{
    [XmlRoot("sitemapindex", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SiteMapIndex
    {
        public SiteMapIndex()
        {
            SiteMaps = new List<SiteMap>();
        }

        [XmlElement("sitemap")]
        public List<SiteMap> SiteMaps { get; set; }
    }

    public class SiteMap
    {
        [XmlElement("lastmod")]
        public DateTime LastModified { get; set; }

        [XmlElement("loc")]
        public string Location { get; set; }
    }
}
