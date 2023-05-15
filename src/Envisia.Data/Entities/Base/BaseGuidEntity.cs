using System.ComponentModel.DataAnnotations;

namespace Envisia.Data.Entities.Base
{
    public class BaseGuidEntity : AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
