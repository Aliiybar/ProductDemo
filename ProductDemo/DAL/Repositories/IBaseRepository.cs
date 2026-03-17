namespace ProductDemo.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        Task<int> SaveChangesAsync();
    }
}
