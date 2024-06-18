using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.DataAccess;

namespace TopShop.Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        private readonly TopShopContext _context;
        public CreateCategoryValidator(TopShopContext context)
        {
            _context = context;

            RuleFor(x => x.Name)    
                .NotEmpty().WithMessage("Product Name is required.")
                .Must(n => !_context.Categories.Any(p => p.Name == n))
                .WithMessage(p => $"Product with name ${p.Name} already exists.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product Description is required.");
        }
    }
}
