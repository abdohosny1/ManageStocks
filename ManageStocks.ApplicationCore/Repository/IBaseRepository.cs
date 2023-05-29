using System;


namespace ManageStocks.ApplicationCore.Repository
{
    public interface IBaseRepository<T> where T:class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GellAllAsync(params Expression<Func<T, object>>[] includeProperty);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
