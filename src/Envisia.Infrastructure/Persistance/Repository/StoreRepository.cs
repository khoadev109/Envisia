using Envisia.Data.Entities;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Library.Persistance;

namespace Envisia.Infrastructure.Persistance.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
