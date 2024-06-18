using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;

namespace TopShop.Application.UseCases.Queries
{
    public interface IGetUsersQuery : IQuery<UserSearchDTO, PagedResponse<UserDTO>>
    {
    }

    public interface IFindUserQuery : IQuery<int, UserDTO> { }
}
