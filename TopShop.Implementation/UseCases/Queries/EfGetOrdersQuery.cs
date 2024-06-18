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

    public class EfGetOrdersQuery : EfUseCase, IGetOrdersQuery
    {
        public EfGetOrdersQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Get Orders";

        public PagedResponse<OrderDTO> Execute(OrderSearchDTO search)
        {
            IQueryable<Order> query = Context.Orders.AsQueryable();
            if (search.UserId != null)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }
            if (search.TotalAmountFrom != null)
            {
                query = query.Where(x => x.TotalAmount > search.TotalAmountFrom);
            }
            if (search.TotalAmountTo != null)
            {
                query = query.Where(x => x.TotalAmount < search.TotalAmountTo);
            }

            query = query.Where(x => x.IsActive);
            

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<OrderDTO> response = new()
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new OrderDTO
                {
                    Id = x.Id,
                    Username = x.User.Username,
                    CreatedAt = x.CreatedAt,
                    OrderItems = x.OrderItems.Select(x => new OrderItemDTO 
                    { 
                        ProductName = x.ProductName,
                        UnitPrice= x.UnitPrice,
                        Quantity = x.Quantity,
                    }).ToList()
                }).ToList(),
            };

            return response;
        }
    }
}
