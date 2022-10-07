using Microsoft.EntityFrameworkCore;
using PersonsManager.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PersonsManager.Database.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected PersonsManagerDbContext dbContext;

        public BaseRepository(PersonsManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateEntity(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void DeleteEntity(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAllEntities(bool trackChanges)
        {
            return !trackChanges ? dbContext.Set<T>().AsNoTracking() : dbContext.Set<T>();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                dbContext.Set<T>().Where(expression).AsNoTracking() :
                dbContext.Set<T>().Where(expression);
        }

        public void UpdateEntity(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
    }
}
