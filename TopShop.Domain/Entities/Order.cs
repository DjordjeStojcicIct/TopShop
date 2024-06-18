using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Domain.Entities
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public decimal TotalAmount { get; set; }

        public int ShippingAddressId { get; set; }
        public virtual Address ShippingAddress { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
