using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Exceptions;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCases.Queries;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Queries
{
   
    public class EfFindAddressQuery: EfUseCase, IFindAddressQuery     
    {
        public EfFindAddressQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 14;

        public string Name => "Find Address";

        public AddressDTO Execute(int search)
        {
            Address? a = Context.Addresses.Find(search);

            if (a == null)
            {
                throw new EntityNotFoundException(search,nameof(Address));
            }
            return new AddressDTO
            {
                City = a.City,
                State = a.State,
                Street = a.Street,
                PostalCode = a.PostalCode,
                Country = a.Country,
            };
        }
    }

    public class EfFindCategoryQuery : EfUseCase, IFindCategoryQuery
    {
        public EfFindCategoryQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 15;

        public string Name => "Find Category";

        public CategoryDTO Execute(int search)
        {
            Category? a = Context.Categories.Find(search);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new CategoryDTO
            {
                Name = a.Name,
                Description = a.Description,
            };
        }
    }

    public class EfFindOrderQuery : EfUseCase, IFindOrderQuery
    {
        public EfFindOrderQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 16;

        public string Name => "Find Order";

        public OrderDTO Execute(int search)
        {
            Order? a = Context.Orders.Include(x=>x.User).FirstOrDefault(y=>y.Id == search);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new OrderDTO
            {
                Id = a.Id,
                Username = a.User.Username,
                CreatedAt= a.CreatedAt,
                OrderItems = a.OrderItems.Select(x=>new OrderItemDTO
                {
                    ProductName = x.ProductName,
                    Quantity= x.Quantity,
                    UnitPrice= x.UnitPrice,
                }).ToList(),
            };
        }
    }

    public class EfFindProductQuery : EfUseCase, IFindProductQuery
    {
        public EfFindProductQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 17;

        public string Name => "Find Product";

        public ProductDTO Execute(int search)
        {
            Product? a = Context.Products.Include(x => x.Category).Include(x=>x.ProductImage)
                .FirstOrDefault(y => y.Id == search);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new ProductDTO
            {
                Name = a.Name,
                Category = a.Category.Name,
                Description= a.Description,
                Price  = a.Price,
                ProductImageFile = a.ProductImage?.Path,
                StockCount = a.StockQuantity,
            };
        }
    }

    public class EfFindReviewQuery : EfUseCase, IFindReviewQuery
    {
        public EfFindReviewQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 18;

        public string Name => "Find Review";

        public ReviewDTO Execute(int search)
        {
            Review? a = Context.Reviews.Include(x=>x.User).Include(x=>x.Product)
                .FirstOrDefault(y => y.Id == search);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new ReviewDTO
            {
                Username = a.User.Username, 
                Comment = a.Comment,
                ProductName = a.Product.Name,
                Rating= a.Rating,
            };
        }
    }

    public class EfFindRoleQuery : EfUseCase, IFindRoleQuery
    {
        public EfFindRoleQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 19;

        public string Name => "Find Role";

        public RoleDTO Execute(int search)
        {
            Role? a = Context.Roles
                .FirstOrDefault(y => y.Id == search);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new RoleDTO
            {
                Name = a.Name,
            };
        }
    }

    public class EfFindUserQuery : EfUseCase, IFindUserQuery
    {
        public EfFindUserQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 20;

        public string Name => "Find User";

        public UserDTO Execute(int search)
        {
            User? a = Context.Users.Include(x=>x.Role)
                .FirstOrDefault(y => y.Id == search);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new UserDTO
            {
                Username = a.Username,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Role = a.Role.Name,
            };
        }
    }

    public class EfFindWishlistQuery : EfUseCase, IFindWishListQuery
    {
        public EfFindWishlistQuery(TopShopContext context) : base(context)
        {
        }
        public int Id => 21;

        public string Name => "Find Wishlist";

        public WishlistDTO Execute(int search)
        {
            Wishlist? a = Context.Wishlists.Include(x => x.Product).Include(x=>x.User)
                .FirstOrDefault(y => y.Id == search && y.IsActive);

            if (a == null)
            {
                throw new EntityNotFoundException(search, nameof(Address));
            }
            return new WishlistDTO
            {
                Username = a.User.Username,
                ProductName = a.Product.Name,
            };
        }
    }


}
