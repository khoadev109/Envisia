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

        public async Task<IEnumerable<News>> GetByOrganisationIdAsync(int organisationId)
        {
            IEnumerable<News> news = await GetAllAsync(x => x.OrganisationId == organisationId);

            return news;
        }

        public async Task<IEnumerable<News>> GetByFormulaIdAsync(int formulaId)
        {
            IEnumerable<News> news = await GetAllAsync(x => x.FormulaId == formulaId);

            return news;
        }

        public async Task<IEnumerable<News>> GetByStoreIdAsync(int storeId)
        {
            IEnumerable<News> news = await GetAllAsync(x => x.StoreId == storeId);

            return news;
        }
    }
}
