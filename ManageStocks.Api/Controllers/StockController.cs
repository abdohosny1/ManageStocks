using ManageStocks.Api.Helper;
using ManageStocks.ApplicationCore.DTO;
using ManageStocks.ApplicationCore.Model;
using ManageStocks.ApplicationCore.unitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ManageStocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<SocketHub> _hubContext;


        public StockController(IUnitOfWork unitOfWork, IHubContext<SocketHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        [HttpPost("updateprice")]
        public async Task<IActionResult> UpdatePrice()
        {
            await _hubContext.Clients.All.SendAsync("UpdatePrice");
            return Ok();
        }

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
