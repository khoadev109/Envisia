using Envisia.Data.Entities;
using Envisia.Library.Persistance;

namespace Envisia.Data.Interfaces.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetByOrganisationIdAsync(int organisationId);

        Task<IEnumerable<News>> GetByFormulaIdAsync(int formulaId);

        Task<IEnumerable<News>> GetByStoreIdAsync(int storeId);
    }
}
