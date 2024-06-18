using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.CommandDTO
{
    public class EditCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get;set; }
    }
}
