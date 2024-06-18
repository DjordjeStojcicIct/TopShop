using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.CommandDTO;

namespace TopShop.Application.UseCases.Commands
{
    public interface IEditCategoryCommand: ICommand<EditCategoryDTO>
    {
    }
}
