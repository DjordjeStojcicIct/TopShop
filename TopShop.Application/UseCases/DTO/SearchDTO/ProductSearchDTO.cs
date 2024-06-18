using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.SearchDTO
{
    public class ProductSearchDTO : SearchDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
