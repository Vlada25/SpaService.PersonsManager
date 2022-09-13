using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Repositories;

namespace PersonsManager.Database.Repositories
{
    public class MastersRepository : BaseRepository<Master>, IMastersRepository
    {
        public MastersRepository(PersonsManagerDbContext dbContext)
            : base(dbContext) { }

        public void Create(Master entity) => CreateEntity(entity);

        public IEnumerable<Master> GetAll(bool trackChanges) =>
            GetAllEntities(trackChanges);

        public Master GetById(Guid id, bool trackChanges) =>
            GetByCondition(fm => fm.Id.Equals(id), trackChanges).SingleOrDefault();

        public Master GetByUserId(Guid userId) =>
            GetByCondition(c => c.UserId.Equals(userId), false).SingleOrDefault();

        public void Delete(Master entity) => DeleteEntity(entity);
    }
}
