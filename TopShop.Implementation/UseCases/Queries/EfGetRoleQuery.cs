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

    public class EfGetRoleQuery : EfUseCase, IGetRolesQuery
    {
        public EfGetRoleQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Get Roles";

        public PagedResponse<RoleDTO> Execute(RoleSearchDTO search)
        {
            IQueryable<Role> query = Context.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<RoleDTO> response = new PagedResponse<RoleDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new RoleDTO
                {
                    Name = x.Name
                }).ToList()
            };

            return response;
        }
    }
}
