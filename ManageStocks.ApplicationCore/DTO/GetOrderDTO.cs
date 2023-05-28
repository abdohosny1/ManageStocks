using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.ApplicationCore.DTO
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public string? PersonName { get; set; }
        public int Quentity { get; set; }
        public decimal Price { get; set; }

        public string? StockName { get; set; }
    }
}
