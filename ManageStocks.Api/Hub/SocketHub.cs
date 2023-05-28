using ManageStocks.ApplicationCore.Model;
using ManageStocks.ApplicationCore.unitOfWork;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace ManageStocks.Api.Helper
{
    public class SocketHub : Hub
    {
        //private readonly IServiceProvider _serviceProvider; 
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        private readonly Random _random;
        public SocketHub(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _random = new Random();
            _unitOfWork = unitOfWork;

            //   _unitOfWork = unitOfWork;
            //_serviceProvider = serviceProvider;
            //_dbContext = dbContext;
        }

        //public override async Task OnConnectedAsync()
        //{
        //    try
        //    {
        //        await base.OnConnectedAsync();

        //        // Start sending stock price updates every 10 seconds
        //        while (true)
        //        {
        //            var stocks = await _dbContext.Stocks.ToListAsync();
        //            foreach (var stock in stocks)
        //            {
        //                stock.Price = _random.Next(1, 100);
        //            }
        //            await _dbContext.SaveChangesAsync();
        //            await Clients.All.SendAsync("updateAllPrice", stocks);
        //            await Task.Delay(TimeSpan.FromSeconds(10));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"SignalR connection error: {ex.Message}");
        //    }
        //}

        public override async Task OnConnectedAsync()
        {
            try
            {
                await base.OnConnectedAsync();

                // Start sending stock price updates every 10 seconds
                while (true)
                {
                    var stocks = await _unitOfWork.Stocks.GetAll();   ///_unitOfWork.Stocks.GetAll().Result; // Make sure to await the GetAll() method
                    if (stocks == null)
                    {
                        return;
                    }

                    foreach (var stock in stocks)
                    {
                        stock.Price = _random.Next(1, 101); 
                    }
                    await  _unitOfWork.CommitAsync();
                    await Clients.All.SendAsync("updateAllPrice", stocks);

                    await Task.Delay(10000); // Delay for 10 seconds
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" connection error: {ex.Message}");
            }
        }

        //public async Task SendUpdatedStocks(List<Stock> stocks)
        //    {
        //        await Clients.All.SendAsync("ReceiveUpdatedStocks", stocks);
        //    }

        //    public async Task SendStockPriceUpdates()
        //    {
        //        while (true)
        //        {
        //            var stocks = _unitOfWork.Stocks.GetAll().Result; // Make sure to await the GetAll() method
        //            if (stocks == null)
        //            {
        //                return;
        //            }

        //            foreach (var stock in stocks)
        //            {
        //                stock.Price = _random.Next(1, 101); // Update the price to a random value
        //                _unitOfWork.Stocks.Update(stock);
        //            }
        //            _unitOfWork.Commit();
        //            await Clients.All.SendAsync("updateAllPrice", stocks);

        //            await Task.Delay(10000); // Delay for 10 seconds
        //        }
        //    }
        //public async Task SendStockPriceUpdate(decimal price)
        //{
        //    await Clients.All.SendAsync("ReceiveStockPriceUpdate", price);
        //}
        //public async Task UpdatePrice()
        //{
        //    decimal price = new Random().Next(1, 101);

        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //        var socket = await dbContext.Stocks.FirstOrDefaultAsync();
        //        if (socket != null)
        //        {
        //            socket.Price = price;
        //            await dbContext.SaveChangesAsync();
        //        }
        //    }

        //    await Clients.All.SendAsync("UpdatePrice", price);
        //}

        ////public async Task SendStockPriceUpdates()
        ////{
        ////    while (true)
        ////    {
        ////        int stockId = 1; // Assuming you want to update stock with Id 1
        ////        var stock = await _dbContext.Stocks.FindAsync(stockId);
        ////        if (stock != null)
        ////        {
        ////            stock.Price = _random.Next(1, 101); // Update the price to a random value
        ////            _dbContext.Entry(stock).State = EntityState.Modified;
        ////            await _dbContext.SaveChangesAsync();

        ////            await Clients.All.SendAsync("ReceiveStockPriceUpdate", stock);
        ////        }

        ////        await Task.Delay(10000); // Delay for 10 seconds
        ////    }
        ////}
        //public async Task UpdateStockPrices(List<Stock> stocks)
        //{
        //    await Clients.All.SendAsync("StockPriceUpdate", stocks);
        //}
        //public async Task SendStockPriceUpdates()
        //{
        //    while (true)
        //    {
        //        var stock = await _dbContext.Stocks.FirstOrDefaultAsync();
        //        if (stock != null)
        //        {
        //            stock.Price = _random.Next(1, 101); // Update the price to a random value
        //            await _dbContext.SaveChangesAsync();

        //            //  await Clients.All.SendAsync("ReceiveStockPriceUpdate", stock);
        //        }

        //        await Task.Delay(10000); // Delay for 10 seconds
        //    }
        //}
    }
}