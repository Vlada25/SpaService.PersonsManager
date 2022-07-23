using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Delete(Master entity) => DeleteEntity(entity);
    }
}
