using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application;
using TopShop.Application.Exceptions;
using TopShop.Application.UseCases.Commands;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfDeleteOrderCommand : EfUseCase, IDeleteOrderCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteOrderCommand(TopShopContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 24;

        public string Name => "Delete Order";

        public void Execute(int request)
        {
            Order? o = Context.Orders.Find(request);

            if(o == null) 
            {
                throw new EntityNotFoundException(Id, nameof(Order));
            }

            o.IsActive = false;
            o.DeletedAt = DateTime.UtcNow;
            o.DeletedBy = $"{_actor.Id} - {_actor.Email}";

            Context.SaveChanges();
        }
    }
}
