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
    public class EfGetProductsQuery : EfUseCase, IGetProductsQuery
    {
        public EfGetProductsQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Get Products";

        public PagedResponse<ProductDTO> Execute(ProductSearchDTO search)
        {
            IQueryable<Product> query = Context.Products.AsQueryable();
            if(!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name)) 
            {
                query = query.Where(x=>x.Name.ToLower().Contains(search.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.Description) || !string.IsNullOrWhiteSpace(search.Description))
            {
                query = query.Where(x => x.Description.ToLower().Contains(search.Description.ToLower()));
            }
            if (search.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == search.CategoryId);
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<ProductDTO> response = new PagedResponse<ProductDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new ProductDTO {
                    Name= x.Name,
                    Description= x.Description,
                    Category = x.Category.Name,
                    Price = x.Price,
                    StockCount = x.StockQuantity,
                }).ToList()
            };

            return response;
        }
    }
}
