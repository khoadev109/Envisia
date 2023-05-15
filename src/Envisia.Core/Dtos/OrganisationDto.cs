namespace Envisia.Application.Dtos
{
    public class OrganisationDto : BaseDto
    {
        public string Name { get; set; }

        public int? LogoId { get; set; }

        public LogoDto Logo { get; set; }
    }
}
