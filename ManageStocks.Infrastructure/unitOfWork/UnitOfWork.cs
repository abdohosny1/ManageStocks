using ManageStocks.ApplicationCore.Model;
using ManageStocks.ApplicationCore.Repository;
using ManageStocks.ApplicationCore.unitOfWork;
using ManageStocks.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.Infrastructure.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Stocks=new BaseRepositories<Stock>(_context);
            Orders=new BaseRepositories<Order>(_context);
        }

        public IBaseRepository<Stock> Stocks { get; private set; }

        public IBaseRepository<Order> Orders { get; private set; }


        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
             await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

      
    }
}
