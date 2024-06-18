using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO;
using TopShop.Application.UseCases.DTO.QueryDTO;

namespace TopShop.Implementation.Validators
{
    public class AddressValidator : AbstractValidator<AddressDTO>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("PostalCode is required.");
        }
    }
}
