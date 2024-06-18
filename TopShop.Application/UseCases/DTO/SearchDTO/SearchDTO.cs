using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.SearchDTO
{
    public abstract class SearchDTO
    {
        public int PerPage { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}
