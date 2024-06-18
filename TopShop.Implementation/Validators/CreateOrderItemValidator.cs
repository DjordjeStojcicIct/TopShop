using FluentValidation;
using System.Runtime.CompilerServices;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.DataAccess;

namespace TopShop.Implementation.Validators;

public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDTO>
{
    private TopShopContext _context;
    public CreateOrderItemValidator(TopShopContext context) 
    {
        _context = context;

        RuleFor(x => x.ProductId).Must(x => _context.Products.Any(y => y.Id == x))
            .WithMessage("Product with this id do not exists.");

        RuleFor(y => y.Quantity)
                .GreaterThan(0)
                .WithMessage("There must be at least one product selected");
    }
}
