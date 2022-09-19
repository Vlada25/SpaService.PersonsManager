using PersonsManager.Domain.Models;

namespace PersonsManager.Interfaces.Repositories
{
    public interface IMastersRepository
    {
        Task<IEnumerable<Master>> GetAll(bool trackChanges);
        Task<Master> GetById(Guid id, bool trackChanges);
        Task<Master> GetByUserId(Guid userId);
        Task Create(Master entity);
        void Delete(Master entity);
        void Update(Master entity);
    }
}
