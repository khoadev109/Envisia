namespace Envisia.Application.Dtos
{
    public class FormulaDto : BaseDto
    {
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public int? LogoId { get; set; }

        public LogoDto Logo { get; set; }
    }
}
