namespace Envisia.Application.Dtos
{
    public class UserRoleDto : BaseDto
    {
        public int? ClientId { get; set; }

        public int? UserId { get; set; }

        public int? RoleId { get; set; }
    }
}
