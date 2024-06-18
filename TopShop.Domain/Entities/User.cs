using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Domain.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int AddressId{ get; set; }
        public virtual Address Address { get; set; }

        public int? ProfileImageId { get; set; }
        public virtual FileT? ProfileImage { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new HashSet<AuditLog>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
    }
}
