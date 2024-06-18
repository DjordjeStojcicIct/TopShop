using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int StockQuantity { get; set; }    

        public int? ProductImageId { get; set; }
        public virtual FileT? ProductImage { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
    }
}
