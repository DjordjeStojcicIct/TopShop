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
    public class CreateReviewValidator : AbstractValidator<CreateReviewDTO>
    {
        private TopShopContext _context;
        public CreateReviewValidator(TopShopContext context) 
        {
            _context = context;
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Message is required.");
            RuleFor(x => x.Rating).NotNull().WithMessage("Rating is required.").Must(x=>x>0 && x<=5)
                .WithMessage("Rating must be number from 1 to 5");
            RuleFor(x => x.ProductId).Must(x => _context.Products.Any(y => y.Id == x))
                .WithMessage("Product with this id do not exists");
        }
    }
}
