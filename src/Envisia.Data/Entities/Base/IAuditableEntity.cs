namespace Envisia.Data.Entities.Base
{
    public interface IAuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedDateTime { get; set; }
    }
}
