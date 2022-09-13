using PersonsManager.Domain.Models;

namespace PersonsManager.Interfaces.Repositories
{
    public interface IMastersRepository
    {
        IEnumerable<Master> GetAll(bool trackChanges);
        Master GetById(Guid id, bool trackChanges);
        Master GetByUserId(Guid userId);
        void Create(Master entity);
        void Delete(Master entity);
    }
}
