using Microsoft.AspNetCore.SignalR;
using System;

namespace ManageStocks.Api.Helper
{
    public class SocketHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;

        public SocketHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task UpdatePrice()
        {
            decimal price = new Random().Next(1, 101);

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var socket = await dbContext.Stocks.FirstOrDefaultAsync();
                if (socket != null)
                {
                    socket.Price = price;
                    await dbContext.SaveChangesAsync();
                }
            }

            await Clients.All.SendAsync("PriceUpdated", price);
        }
    }
}