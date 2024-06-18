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
    public class EfGetCategories : EfUseCase, IGetCategoriesQuery
    {
        public EfGetCategories(TopShopContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Get Categories";

        public PagedResponse<CategoryDTO> Execute(CategoriesSearchDTO search)
        {
            IQueryable<Category> query = Context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.Description) || !string.IsNullOrWhiteSpace(search.Description))
            {
                query = query.Where(x => x.Description.ToLower().Contains(search.Description.ToLower()));
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<CategoryDTO> response = new PagedResponse<CategoryDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new CategoryDTO
                {
                    Name = x.Name,
                    Description = x.Description,
                }).ToList(),
            };

            return response;
        }
    }
}
