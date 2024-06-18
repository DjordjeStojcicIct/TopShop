using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.QueryDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderItemDTO> OrderItems {get;set;} = new HashSet<OrderItemDTO>();
    }
}
