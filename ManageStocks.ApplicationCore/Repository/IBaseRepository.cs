using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
