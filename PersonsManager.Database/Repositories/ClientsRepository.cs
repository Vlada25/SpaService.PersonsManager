using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Database.Repositories
{
    public class ClientsRepository : BaseRepository<Client>, IClientsRepository
    {
        public ClientsRepository(PersonsManagerDbContext dbContext)
            : base(dbContext) { }

        public void Create(Client entity) => CreateEntity(entity);

        public IEnumerable<Client> GetAll(bool trackChanges) =>
            GetAllEntities(trackChanges);

        public Client GetByUserId(Guid userId) =>
            GetByCondition(c => c.UserId.Equals(userId), false).SingleOrDefault();

        public Client GetById(Guid id, bool trackChanges) =>
            GetByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefault();

        public void Delete(Client entity) => DeleteEntity(entity);
    }
}
