using Microsoft.EntityFrameworkCore;
using PersonsManager.Domain.Models;
using PersonsManager.Interfaces.Repositories;

namespace PersonsManager.Database.Repositories
{
    public class MastersRepository : BaseRepository<Master>, IMastersRepository
    {
        public MastersRepository(PersonsManagerDbContext dbContext)
            : base(dbContext) { }

        public async Task Create(Master entity) => await CreateEntity(entity);

        public async Task<IEnumerable<Master>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).ToListAsync();

        public async Task<Master> GetById(Guid id, bool trackChanges) =>
            await GetByCondition(m => m.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<Master> GetByUserId(Guid userId) =>
            await GetByCondition(m => m.UserId.Equals(userId), false).SingleOrDefaultAsync();

        public void Delete(Master entity) => DeleteEntity(entity);

        public void Update(Master entity) =>
            UpdateEntity(entity);

        public async Task<IEnumerable<Master>> GetByAddressId(Guid addressId, bool trackChanges) =>
            await GetByCondition(m => m.AddressId.Equals(addressId), trackChanges).ToListAsync();
    }
}
