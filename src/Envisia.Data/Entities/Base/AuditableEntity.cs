namespace Envisia.Data.Entities.Base
{
    public class AuditableEntity : IAuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; } = string.Empty;

        public DateTime? ModifiedDateTime { get; set; } = null;

        public string? DeletedBy { get; set; } = string.Empty;

        public DateTime? DeletedDateTime { get; set; } = null;
    }
}
