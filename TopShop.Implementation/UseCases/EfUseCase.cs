using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.DataAccess;

namespace TopShop.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected TopShopContext Context { get; }

        protected EfUseCase(TopShopContext context)
        {
            Context = context;
        }
    }
}
