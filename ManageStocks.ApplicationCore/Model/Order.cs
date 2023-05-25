using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.ApplicationCore.Model
{
    public class Order : BaseModel
    {
        public string? PersonName { get; set; }
        public int Quentity { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

    }
}
