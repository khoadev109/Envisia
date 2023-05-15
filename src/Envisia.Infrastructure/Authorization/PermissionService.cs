using Envisia.Application.Interfaces.Authorization;
using Envisia.Infrastructure.Persistance;

namespace Envisia.Infrastructure.Authorization
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _appDbContext;

        public PermissionService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
