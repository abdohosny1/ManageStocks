using ManageStocks.ApplicationCore.Model;
using ManageStocks.ApplicationCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.ApplicationCore.unitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<Stock> Stocks { get; } 
        IBaseRepository<Order> Orders { get; }

        int Commit();
        Task CommitAsync();
        void Dispose();
    }
}
