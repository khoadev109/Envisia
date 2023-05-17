using Envisia.Data.Entities;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Library.Persistance;

namespace Envisia.Infrastructure.Persistance.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
