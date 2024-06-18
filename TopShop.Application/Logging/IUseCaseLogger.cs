using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases;

namespace TopShop.Application.Logging
{
    public interface IUseCaseLogger
    {
        void Log(UseCaseLogEntry i);
    }

    public class UseCaseLogEntry
    {
        public string Actor { get; set; }
        public int ActorId { get; set; }
        public string UseCaseName { get; set; }
    }
}
