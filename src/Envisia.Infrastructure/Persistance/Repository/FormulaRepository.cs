using Envisia.Data.Entities;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Library.Persistance;

namespace Envisia.Infrastructure.Persistance.Repository
{
    public class FormulaRepository : Repository<Formula>, IFormulaRepository
    {
        public FormulaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
