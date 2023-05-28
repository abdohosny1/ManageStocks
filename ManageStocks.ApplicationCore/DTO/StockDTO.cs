using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.ApplicationCore.DTO
{
    public class StockDTO
    {

        [Required(ErrorMessage ="Enter Name Stock")]
        public string? Name { get; set; }

       // public decimal Price { get; set; }


    }
}
