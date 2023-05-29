


namespace ManageStocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
     



        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

    
       

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _unitOfWork.Stocks.GetAll());

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
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
                Price=0,
            };
            _unitOfWork.Stocks.Add(stock);
            _unitOfWork.Commit();
            return NoContent();
        }

        //[HttpPut("id")]
        //public async Task<IActionResult> Update(int id,StockDTO entity)
        //{
        //    var stock = await _unitOfWork.Stocks.GetById(id);
        //    if (stock is null) return NotFound();
        //    stock.Name = entity.Name;
            
        //    _unitOfWork.Stocks.Update(stock);
        //    _unitOfWork.Commit();
        //    return Ok(stock);
        //}

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
