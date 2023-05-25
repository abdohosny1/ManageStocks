using ManageStocks.ApplicationCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.Infrastructure.Repositories
{
    public class BaseRepositories<T> : IBaseRepository<T> where  T :class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
           _context.Set<T>().Add(entity);
           // _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
           // _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
          return  await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity!;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
           // _context.SaveChanges();
        }
    }
}
