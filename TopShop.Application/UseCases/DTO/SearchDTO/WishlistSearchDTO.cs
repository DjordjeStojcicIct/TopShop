using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.SearchDTO
{
    public class WishlistSearchDTO : SearchDTO
    {
        public string? Username { get; set; }
        public string? ProductName { get; set; }
    }
}
