using PersonsManager.Domain.Models;

namespace PersonsManager.Interfaces.Repositories
{
    public interface IClientsRepository
    {
        IEnumerable<Client> GetAll(bool trackChanges);
        Client GetById(Guid id, bool trackChanges);
        Client GetByUserId(Guid userId);
        void Create(Client entity);
        void Delete(Client entity);
    }
}
