using FluentValidation;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.DataAccess;

namespace TopShop.Implementation.Validators;

public class AddProductValidator : AbstractValidator<CreateProductDTO>
{
    private readonly TopShopContext _context;
    public AddProductValidator(TopShopContext context) 
    {
        _context = context;

        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Product Name is required.")
            .Must(n => !_context.Products.Any(p =>p.Name == n))
            .WithMessage(p => $"Product with name ${p.Name} already exists.");

        RuleFor(x=>x.Description)
            .NotEmpty().WithMessage("Product Description is required.");

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("Product Price is required.")
            .Must(x => x > 0)
            .WithMessage("Price must be a valid number.");

        RuleFor(x => x.StockCount)
            .NotNull()
            .WithMessage("Product Stock Count is required.")
            .Must(x => x >= 0)
            .WithMessage("Stock Count must be equal or greater than 0.");

        RuleFor(x => x.CategoryId).NotNull().WithMessage("Category Id is required.")
            .Must(p => _context.Categories.Any(c => c.Id == p))
            .WithMessage("Category doesn't exists.");
    }
}
