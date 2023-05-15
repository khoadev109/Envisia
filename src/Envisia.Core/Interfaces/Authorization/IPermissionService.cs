namespace Envisia.Application.Interfaces.Authorization
{
    public interface IPermissionService
    {
        Task<bool> CheckAsAdminAsync(int userId);

        Task<bool> CheckAsUserAsync(int userId);

        Task<bool> CheckAsAdminOrUserAsync(int userId);
    }
}
