

namespace ManageStocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;


        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _unitOfWork.Orders.GellAllAsync(e=>e.Stock);
            var result = orders.Select(e => new {
                Quentity = e.Quentity,
                PersonName = e.PersonName,
                Price=e.Price,
                Id = e.Id,
                StockName = e.Stock.Name

            });
            return Ok(result);
        }
        [HttpGet("id")]

        public async Task<IActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order is null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderDTO entity)
        {
            var socket = await _unitOfWork.Stocks.GetById(entity.StockId);
            if (socket is null) return NotFound();
            Order order = new()
            {
                PersonName = entity.PersonName,
                Price = socket.Price*entity.Quentity,
                Quentity = entity.Quentity,
                StockId = entity.StockId
            };
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Commit();

            return Ok(order);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, OrderDTO entity)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order is null) return NotFound();

            var socket = await _unitOfWork.Stocks.GetById(entity.StockId);
            if (socket is null) return NotFound();

            order.Quentity = entity.Quentity;
            order.StockId = entity.StockId;
            order.Price = socket.Price * entity.Quentity;
            order.PersonName = entity.PersonName;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order is null) return NotFound();

            _unitOfWork.Orders.Delete(order);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpGet("name")]
        public async Task<ActionResult> GetOrderBYStock(string name)
        {
            if (name == null) return BadRequest();

            var Allorders = await _unitOfWork.Orders.GellAllAsync(e=>e.Stock);
            var orders = Allorders.Where(e => e.Stock.Name == name).ToList();
            if (orders == null)
                return NotFound();

            var result = orders.Select(e => new {
                Quentity = e.Quentity,
                PersonName = e.PersonName,
                Price = e.Price,
                Id = e.Id,
                StockName = e.Stock?.Name

            });

            return Ok(result);
        }
    }
}
