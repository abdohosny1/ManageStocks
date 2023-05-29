

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
