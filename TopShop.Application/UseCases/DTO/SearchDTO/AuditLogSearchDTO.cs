using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.UseCases.DTO.SearchDTO
{
    public class AuditLogSearchDTO : SearchDTO
    {
        public int? UserId { get; set; }
        public string? UseCaseName { get; set; }
        public string? UseCaseData { get; set; }
    }
}
