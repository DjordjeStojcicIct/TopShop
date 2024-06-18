using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.QueryDTO
{
    public class AuthRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
