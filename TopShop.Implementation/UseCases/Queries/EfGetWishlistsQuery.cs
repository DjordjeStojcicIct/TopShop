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
    public class EfGetWishlistsQuery : EfUseCase, IGetWishlistsQuery
    {

        public EfGetWishlistsQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 13;

        public string Name => "Get Wishlists";

        public PagedResponse<WishlistDTO> Execute(WishlistSearchDTO search)
        {
            IQueryable<Wishlist> query = Context.Wishlists.AsQueryable();
            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.User.Username.ToLower().Contains(search.Username.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.ProductName) || !string.IsNullOrWhiteSpace(search.ProductName))
            {
                query = query.Where(x => x.Product.Name.ToLower().Contains(search.ProductName.ToLower()));
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<WishlistDTO> response = new PagedResponse<WishlistDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new WishlistDTO
                {
                    Username = x.User.Username,
                    ProductName = x.Product.Name,
                }).ToList(),
            };

            return response;
        } 
    } 
}


