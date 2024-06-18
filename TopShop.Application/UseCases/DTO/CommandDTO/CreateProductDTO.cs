﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.CreateDTO
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public int CategoryId { get; set; }
        public string? ProductImageFile { get; set; }
    }
}
