using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Domain.Entities;

namespace TopShop.Application.UseCases.DTO.CreateDTO
{
    public class RegisterUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressDTO Address { get; set; }
        public string? ProfileImageFile { get; set; }
    }
}
