using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Exceptions;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CommandDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfEditCategoryCommand : EfUseCase, IEditCategoryCommand
    {
        public EfEditCategoryCommand(TopShopContext context) : base(context)
        {
        }

        public int Id => 27;

        public string Name => "Edit Category";

        public void Execute(EditCategoryDTO request)
        {
            Category? c = Context.Categories.Find(request.Id);

            bool isNameChanged = !request.Name.IsNullOrEmpty();
            bool isDescriptionChanged = !request.Name.IsNullOrEmpty();

            if(!isNameChanged && !isDescriptionChanged) 
            {
                throw new InvalidDataException("No changes made");
            }

            if(c == null)
            {
                throw new EntityNotFoundException(Id, nameof(Order));
            }

            c.Name = isNameChanged? request.Name??c.Name : c.Name;
            c.Description = isDescriptionChanged? request.Description??c.Description : c.Description;
            Context.SaveChanges();
        }
    }
}
