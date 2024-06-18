using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Queries
{

    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Get Users";

        public PagedResponse<UserDTO> Execute(UserSearchDTO search)
        {
            IQueryable<User> query = Context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search.UserName) || !string.IsNullOrWhiteSpace(search.UserName))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.UserName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.FirstName) || !string.IsNullOrWhiteSpace(search.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.LastName) || !string.IsNullOrWhiteSpace(search.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
            }
            if (search.RoleId != null)
            {
                query = query.Where(x => x.RoleId == search.RoleId);
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<UserDTO> response = new PagedResponse<UserDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new UserDTO
                {
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role.Name,
                }).ToList(),
            };

            return response;
        }
    }
}
