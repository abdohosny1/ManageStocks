﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStocks.ApplicationCore.DTO
{
    public class OrderDTO
    {
        
        public string? PersonName { get; set; }
        public int Quentity { get; set; }

        public int StockId { get; set; }
    }
}
