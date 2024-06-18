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
    public class EfDeleteReviewCommand : EfUseCase, IDeleteReviewCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteReviewCommand(TopShopContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 26;

        public string Name => "Delete Review";

        public void Execute(int request)
        {
            Review? o = Context.Reviews.Find(request);

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
