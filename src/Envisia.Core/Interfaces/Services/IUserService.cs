using Envisia.Application.Dtos;
using Envisia.Library;

namespace Envisia.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ServiceResult<bool>> CheckPasswordAsync(string userName, string password);

        Task<ServiceResult<UserDto>> FindByUserNameAsync(string userName);

        Task<ServiceResult<bool>> SaveTenantRefreshTokenAsync(RefreshTokenDto dto);

        Task<ServiceResult<RefreshTokenDto>> GetRefreshTokenAsync();
    }
}
