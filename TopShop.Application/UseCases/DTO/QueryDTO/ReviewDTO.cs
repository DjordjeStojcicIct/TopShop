using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.QueryDTO
{
    public class ReviewDTO
    {
        public string Username { get; set; }
        public string ProductName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
