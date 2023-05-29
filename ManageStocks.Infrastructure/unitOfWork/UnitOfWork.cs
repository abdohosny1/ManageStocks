

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
