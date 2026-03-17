using Microsoft.EntityFrameworkCore;

namespace ProductDemo.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ProductDbContext dalContext;
        protected BaseRepository(ProductDbContext _dalContext)
        {
            dalContext = _dalContext;
        }

        public ProductDbContext Context => dalContext;

        public virtual async Task<T> Get(Guid id)
        {
            var entity = await dalContext.Set<T>().FindAsync(id);
            return entity!;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = dalContext.Set<T>();
            return query;
        }

        public virtual void Add(T entity)
        {
            dalContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            dalContext.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            dalContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await dalContext.SaveChangesAsync();
        }

    }
}
