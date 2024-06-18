using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Uploads;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using TopShop.Implementation.Validators;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase , IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        private readonly IBase64FileUploader _fileUploader;

        public EfRegisterUserCommand(TopShopContext context, RegisterUserValidator validator, IBase64FileUploader fileUploader) : base(context)
        {
            _validator = validator;
            _fileUploader = fileUploader;
        }

        public int Id => 1;

        public string Name => "User Registration";

        public void Execute(RegisterUserDTO request)
        {
            ValidationResult result = _validator.Validate(request);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);  
            }
            Role? role = Context.Roles.FirstOrDefault(x=>x.IsDefault);

            if(role == null)
            {
                throw new InvalidOperationException("Default role doesn't exists.");
            }

            string filePath = "default.jpg";

            if (request.ProfileImageFile != null)
            {
                filePath = _fileUploader.Upload(request.ProfileImageFile, UploadType.User);
            }

            User u = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Address = new Address
                {
                    State = request.Address.State,
                    City = request.Address.City,
                    PostalCode = request.Address.PostalCode,
                    Street= request.Address.Street,
                    Country = request.Address.Country,
                },
                Role = role,
                ProfileImage = new FileT
                {
                    Path= filePath,
                    Size = 100,
                }
            };
            Context.Users.Add(u);
            Context.SaveChanges();
        }
    }
}
