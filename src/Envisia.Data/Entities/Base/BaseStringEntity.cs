using System.ComponentModel.DataAnnotations;

namespace Envisia.Data.Entities.Base
{
    public class BaseStringEntity : AuditableEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
