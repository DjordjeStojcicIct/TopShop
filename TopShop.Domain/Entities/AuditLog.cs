using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Domain.Entities
{
    public class AuditLog : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string UseCaseName { get; set; }
        public string UseCaseData { get; set; }
    }
}
