using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.CommandDTO;
using TopShop.DataAccess;

namespace TopShop.Implementation.Validators
{ 
    public class CreateWishlistValidator : AbstractValidator<CreateWishlistDTO>
    {
        private TopShopContext _context;
        public CreateWishlistValidator(TopShopContext context)
        {
            _context = context;
            RuleFor(x => x.ProductId).Must(x => _context.Products.Any(y => y.Id == x))
                .WithMessage("Product with this id do not exists");
        }
    }
}
