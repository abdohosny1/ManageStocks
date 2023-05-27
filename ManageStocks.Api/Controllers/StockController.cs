using ManageStocks.Api.Helper;
using ManageStocks.ApplicationCore.DTO;
using ManageStocks.ApplicationCore.Model;
using ManageStocks.ApplicationCore.unitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ManageStocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<SocketHub> _hubContext;
        private readonly SocketHub _socketHub;
        private readonly Random _random = new Random();
        private readonly object _lock = new object();
        private Timer _timer;



        public StockController(IUnitOfWork unitOfWork, SocketHub socketHub, IHubContext<SocketHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _socketHub = socketHub;
            _hubContext = hubContext;
        }

        [HttpPost("updateprice")]
        public async Task<IActionResult> updateprice()
        {
            // Start the timer to execute the update every 10 seconds
            _timer =  new Timer(UpdateStockPrices, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Ok();
        }
        private  void UpdateStockPrices(object state)
        {
            lock (_lock)
            {
                var stocks =  _unitOfWork.Stocks.GetAll(); // Make sure to await the GetAll() method
               // IEnumerable<Stock> stocks = _unitOfWork.Stocks.GetAll(); // Make sure to await the GetAll() method

                if (stocks == null)
                {
                    return;
                }

                //foreach (var stock in stocks)
                //{
                //    stock.Price = _random.Next(1, 101); // Update the price to a random value
                //    _unitOfWork.Stocks.Update(stock);
                //}
                _unitOfWork.Commit();

                // Get an instance of the StockHub and call the UpdateStockPrices method
                _hubContext.Clients.All.SendAsync("ReceiveUpdatedStocks", stocks).Wait();
            }
        }
        //use signlar
        //public async Task<IActionResult> UpdatePrice()
        //{
        //    var stocks = await _unitOfWork.Stocks.GetAll();
        //    if (stocks == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach (var stock in stocks)
        //    {
        //        stock.Price = _random.Next(1, 101); // Update the price to a random value
        //        _unitOfWork.Stocks.Update(stock);
        //    }
        //    _unitOfWork.Commit();

        //    // Get an instance of the StockHub and call the UpdateStockPrices method
        //    await _hubContext.Clients.All.SendAsync("ReceiveUpdatedStocks", stocks as List<Stock>);


        //    return Ok(stocks);
        //}

        //donet use every thing

        //public async Task<IActionResult> UpdatePrice()
        //{
        //    var stocks = await _unitOfWork.Stocks.GetAll();
        //    if (stocks == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach (var stock in stocks)
        //    {
        //        stock.Price = _random.Next(1, 101); // Update the price to a random value
        //        _unitOfWork.Stocks.Update(stock);
        //    }
        //    _unitOfWork.Commit();


        //    return Ok(stocks);

        //}

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
          return Ok( await   _unitOfWork.Stocks.GetAll());
        }

        [HttpGet("id")]
        public async Task<IActionResult>GetById(int id)
        {
            var stock = await _unitOfWork.Stocks.GetById(id);
            if (stock is null) return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        public IActionResult Add(StockDTO entity)
        {
            var stock = new Stock() 
            { 
                Name=entity.Name,
                Price=entity.Price,
            };
            _unitOfWork.Stocks.Add(stock);
            _unitOfWork.Commit();
            return NoContent();
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id,StockDTO entity)
        {
            var stock = await _unitOfWork.Stocks.GetById(id);
            if (stock is null) return NotFound();
            stock.Name = entity.Name;
            stock.Price = entity.Price;
            _unitOfWork.Stocks.Update(stock);
            _unitOfWork.Commit();
            return Ok(stock);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _unitOfWork.Stocks.GetById(id);
            if (stock is null) return NotFound();

            _unitOfWork.Stocks.Delete(stock);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
