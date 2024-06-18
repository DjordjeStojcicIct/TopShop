using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Domain.Entities
{
    public class OrderItem : Entity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
