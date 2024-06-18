using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CommandDTO;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using TopShop.Implementation.Validators;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfCreateWishlistCommand : EfUseCase, ICreateWishlistCommand    
    {
        private readonly CreateWishlistValidator _validator;
        private IApplicationActor _actor;
        public EfCreateWishlistCommand(TopShopContext context, CreateWishlistValidator v, IApplicationActor a) : base(context)
        {
            _validator = v;
            _actor = a;
        }

        public int Id => 2;

        public string Name => "Create Wishlist";

        public void Execute(CreateWishlistDTO request)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            Wishlist w = new Wishlist
            {
                UserId = _actor.Id,
                ProductId = request.ProductId,
            };
            Context.Wishlists.Add(w);
            Context.SaveChanges();
        }
    }
}
