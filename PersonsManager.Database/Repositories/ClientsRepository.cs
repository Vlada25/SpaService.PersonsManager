using Microsoft.EntityFrameworkCore;
using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Repositories;

namespace PersonsManager.Database.Repositories
{
    public class ClientsRepository : BaseRepository<Client>, IClientsRepository
    {
        public ClientsRepository(PersonsManagerDbContext dbContext)
            : base(dbContext) { }

        public async Task Create(Client entity) => await CreateEntity(entity);

        public async Task<IEnumerable<Client>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).ToListAsync();

        public async Task<Client> GetByUserId(Guid userId) =>
            await GetByCondition(c => c.UserId.Equals(userId), false).SingleOrDefaultAsync();

        public async Task<Client> GetById(Guid id, bool trackChanges) =>
            await GetByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void Delete(Client entity) => DeleteEntity(entity);

        public void Update(Client entity) =>
            UpdateEntity(entity);
    }
}
