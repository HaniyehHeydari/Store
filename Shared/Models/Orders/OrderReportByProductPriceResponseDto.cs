﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Orders
{
    public class OrderReportByProductPriceResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public string ProductCategoryName { get; set; }
        public int? TotalSum { get; set; }
    }
}
