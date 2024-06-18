using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using TopShop.Implementation.Validators;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private readonly CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(TopShopContext context, CreateCategoryValidator v) : base(context)
        {
            _validator = v;
        }

        public int Id => 22;

        public string Name => "Create Category";

        public void Execute(CreateCategoryDTO request)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            Category c = new Category
            {
                Name = request.Name,
                Description = request.Description,
            };
            Context.Categories.Add(c);
            Context.SaveChanges();
        }
    }
}
