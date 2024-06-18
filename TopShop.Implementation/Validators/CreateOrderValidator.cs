using FluentValidation;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.DataAccess;

namespace TopShop.Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDTO>
    {
        private readonly TopShopContext _context;
        public CreateOrderValidator(TopShopContext context) 
        {
            _context = context;

            RuleFor(x => x.ShippingAddress).SetValidator(new AddressValidator());

            RuleFor(x => x.OrderItems).NotEmpty().WithMessage("There must be at least one item.")
                .Must(x=>x.Select(i=>i.ProductId).Distinct().Count() == x.Count())
                .WithMessage("Duplicate products are not allowed.")
                .DependentRules(()=>
                {
                    RuleForEach(x => x.OrderItems)
                    .SetValidator(new CreateOrderItemValidator(_context));
                });
            
        }
    }
}
