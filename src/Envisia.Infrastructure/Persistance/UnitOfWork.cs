using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Data.Interfaces.Repositories;
using Envisia.Infrastructure.Persistance.Repository;
using Envisia.Library.Persistance;

namespace Envisia.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed = false;

        private readonly ApplicationDbContext _dbContext;

        private IRepository<Organisation> _organisationRepository;

        private IRepository<User> _userRepository;

        private IRepository<UserRole> _userRoleRepository;

        private IRepository<Client> _clientRepository;

        private IRepository<RefreshToken> _refreshTokenRepository;

        private IStoreRepository _storeRepository;

        private IFormulaRepository _formulaRepository;

        private IRepository<News> _newsRepository;

        private IRepository<Feed> _feedRepository;


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Organisation> OrganisationRepository
        {
            get { return _organisationRepository ??= new Repository<Organisation>(_dbContext); }
        }

        public IRepository<User> UserRepository
        {
            get { return _userRepository ??= new Repository<User>(_dbContext); }
        }

        public IRepository<UserRole> UserRoleRepository
        {
            get { return _userRoleRepository ??= new Repository<UserRole>(_dbContext); }
        }

        public IRepository<Client> ClientRepository
        {
            get { return _clientRepository ??= new Repository<Client>(_dbContext); }
        }

        public IRepository<RefreshToken> RefreshTokenRepository
        {
            get { return _refreshTokenRepository ??= new Repository<RefreshToken>(_dbContext); }
        }

        public IStoreRepository StoreRepository
        {
            get { return _storeRepository ??= new StoreRepository(_dbContext); }
        }

        public IFormulaRepository FormulaRepository
        {
            get { return _formulaRepository ??= new FormulaRepository(_dbContext); }
        }

        public IRepository<News> NewsRepository
        {
            get { return _newsRepository ??= new Repository<News>(_dbContext); }
        }

        public IRepository<Feed> FeedRepository
        {
            get { return _feedRepository ??= new Repository<Feed>(_dbContext); }
        }

        public void Save() => _dbContext.SaveChanges();

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        public void Rollback() => _dbContext.Dispose();

        public async Task RollbackAsync() => await _dbContext.DisposeAsync();


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
