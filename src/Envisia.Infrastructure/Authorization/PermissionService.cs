using Envisia.Application.Interfaces.Authorization;
using Envisia.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Infrastructure.Authorization
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _appDbContext;

        public PermissionService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> CheckAsAdminAsync(int userId)
        {
            bool existed = await _appDbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).AnyAsync(x => x.Id == userId && x.UserRoles.Any(y => y.Role.Name == "Admin"));

            return existed;
        }

        public async Task<bool> CheckAsUserAsync(int userId)
        {
            bool existed = await _appDbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).AnyAsync(x => x.Id == userId && x.UserRoles.Any(y => y.Role.Name == "User"));

            return existed;
        }

        public async Task<bool> CheckAsAdminOrUserAsync(int userId)
        {
            bool existed = await _appDbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).AnyAsync(x => x.Id == userId && x.UserRoles.Any(y => y.Role.Name == "Admin" || y.Role.Name == "User"));

            return existed;
        }
    }
}
