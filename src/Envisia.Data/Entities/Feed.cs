using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Envisia.Data.Entities
{
    public class Feed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string SourceUrl { get; set; }

        public IList<News> NewsList { get; set; } = new List<News>();
    }
}
