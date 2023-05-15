using Envisia.Data.Entities.Base;

namespace Envisia.Data.Entities
{
    public class Contact : BaseEntity
    {
        public string Initials { get; set; }

        public string SurName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string JobTitle { get; set; }

        public string? Phone { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public string? LinkedIn { get; set; } = string.Empty;

        public int? OrganisationId { get; set; }

        public Organisation Organisation { get; set; }

        public int? FormulaId { get; set; }

        public Formula Formula { get; set; }
    }
}
