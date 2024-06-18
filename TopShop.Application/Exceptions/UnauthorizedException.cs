using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.Exceptions
{
    public  class UnauthorizedException : Exception
    {
        public UnauthorizedException(string username, string useCaseName)
            : base($"There was an unauthorized access attempt by {username} for {useCaseName} use case.")
        {

        }
    }
}
