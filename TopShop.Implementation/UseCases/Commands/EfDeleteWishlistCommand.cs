using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Exceptions;
using TopShop.Application.UseCases.Commands;
using TopShop.Application;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfDeleteWishlistCommand : EfUseCase, IDeleteWishlistCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteWishlistCommand(TopShopContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 25;

        public string Name => "Delete Wishlist";

        public void Execute(int request)
        {
            Wishlist? o = Context.Wishlists.Find(request);

            if (o == null)
            {
                throw new EntityNotFoundException(Id, nameof(Wishlist));
            }

            o.IsActive = false;
            o.DeletedAt = DateTime.UtcNow;
            o.DeletedBy = $"{_actor.Id} - {_actor.Email}";

            Context.SaveChanges();
        }
    }
}
