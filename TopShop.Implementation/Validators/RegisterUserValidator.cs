using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Uploads;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.DataAccess;
using TopShop.Implementation.Uploads;

namespace TopShop.Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator(TopShopContext context, IBase64FileUploader uploader) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .Must(x => !context.Users.Any(u => u.Email == x))
                .WithMessage("Email already in use.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Matches("^(?=.{3,12}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Invalid username format. Min 3 characters - allowed letters, digits, . and _")
                .Must(x => !context.Users.Any(u => u.Username == x))
                .WithMessage("Username already in use.");

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password is required.");

            RuleFor(x=>x.Address).SetValidator(new AddressValidator());

            RuleFor(x => x.ProfileImageFile).Must(x => 
                    uploader.IsExtenstionValid(x) 
                    && 
                    new List<string> { "jpg", "png" }.Contains(uploader.GetExtenstion(x))
                )
                .When(x => x.ProfileImageFile != null)
                .WithMessage("Invalid file extesion. Allowed are .jpg and .png");
        }
    }
}
