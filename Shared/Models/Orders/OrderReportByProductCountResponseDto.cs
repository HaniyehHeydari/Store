﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Orders
{
    public class OrderReportByProductCountResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? TotalCount { get; set; }
    }
}
