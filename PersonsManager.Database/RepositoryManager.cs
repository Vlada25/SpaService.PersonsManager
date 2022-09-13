using PersonsManager.Database.Repositories;
using PersonsManager.Interfaces;
using PersonsManager.Interfaces.Repositories;

namespace PersonsManager.Database
{
    public class RepositoryManager : IRepositoryManager
    {
        private PersonsManagerDbContext _dbContext;

        private IMastersRepository _mastersRepository;
        private IClientsRepository _clientsRepository;

        public RepositoryManager(PersonsManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMastersRepository MastersRepository
        {
            get
            {
                if (_mastersRepository == null)
                {
                    _mastersRepository = new MastersRepository(_dbContext);
                }
                return _mastersRepository;
            }
        }

        public IClientsRepository ClientsRepository
        {
            get
            {
                if (_clientsRepository == null)
                {
                    _clientsRepository = new ClientsRepository(_dbContext);
                }
                return _clientsRepository;
            }
        }

        public void Save() => _dbContext.SaveChanges();
    }
}
