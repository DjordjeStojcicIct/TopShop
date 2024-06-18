using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.SearchDTO
{
    public class OrderItemSearchDTO : SearchDTO
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
    }
}
