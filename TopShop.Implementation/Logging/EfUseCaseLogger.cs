using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Logging;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.Logging
{
    public class EfUseCaseLogger : IUseCaseLogger
    {
        private TopShopContext _context;

        public EfUseCaseLogger(TopShopContext context)
        {
            _context = context;
        }

        public void Log(UseCaseLogEntry entry)
        {
            AuditLog log = new AuditLog
            {
                UserId = entry.ActorId,
                UseCaseData = entry.UseCaseName,
                UseCaseName = entry.UseCaseName,
            };

            _context.AuditLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
