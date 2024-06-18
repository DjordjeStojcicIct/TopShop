using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Queries
{
    public class EfGetReviewsQuery : EfUseCase, IGetReviewsQuery
    {
        public EfGetReviewsQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Get Reviews";

        public PagedResponse<ReviewDTO> Execute(ReviewSearchDTO search)
        {
            IQueryable<Review> query = Context.Reviews.AsQueryable();
            if (search.UserId != null)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }
            if (search.ProductId != null)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<ReviewDTO> response = new PagedResponse<ReviewDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new ReviewDTO
                {
                    Username = x.User.Username,
                    ProductName = x.Product.Name,
                    Comment = x.Comment,
                    Rating =x.Rating,
                }).ToList(),
            };

            return response;
        }
    }
}
