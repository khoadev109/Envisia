using Envisia.Data.Entities;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Library.Persistance;

namespace Envisia.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Organisation> OrganisationRepository { get; }

        IRepository<User> UserRepository { get; }

        IRepository<UserRole> UserRoleRepository { get; }

        IRepository<Client> ClientRepository { get; }

        IRepository<RefreshToken> RefreshTokenRepository { get; }

        IStoreRepository StoreRepository { get; }

        IFormulaRepository FormulaRepository { get; }

        IRepository<News> NewsRepository { get; }

        IRepository<Feed> FeedRepository { get; }

        void Save();

        void Rollback();

        Task SaveAsync();

        Task RollbackAsync();
    }
}
