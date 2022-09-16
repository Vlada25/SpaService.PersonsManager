using PersonsManager.Domain.Models;

namespace PersonsManager.Interfaces.Repositories
{
    public interface IClientsRepository
    {
        Task<IEnumerable<Client>> GetAll(bool trackChanges);
        Task<Client> GetById(Guid id, bool trackChanges);
        Task<Client> GetByUserId(Guid userId);
        Task Create(Client entity);
        void Delete(Client entity);
    }
}
