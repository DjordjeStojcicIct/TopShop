using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using TopShop.Implementation.Validators;
using FluentValidation;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfCreateOrderCommand : EfUseCase, ICreateOrderCommand
    {
        private readonly CreateOrderValidator _validator;
        private IApplicationActor _actor;
        public EfCreateOrderCommand(TopShopContext context, CreateOrderValidator v, IApplicationActor a) : base(context)
        {
            _validator = v;
            _actor = a;
        }

        public int Id => 4;

        public string Name => "Create Order";

        public void Execute(CreateOrderDTO request)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            List<OrderItem> list = new List<OrderItem>();

            foreach (CreateOrderItemDTO dto in request.OrderItems)
            {
                list.Add(new OrderItem
                {
                    ProductId = dto.ProductId,
                    ProductName = dto.ProductName,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice,
                });
            }

            Order o = new Order
            {
                ShippingAddress = new Address
                {
                    City = request.ShippingAddress.City,
                    Country = request.ShippingAddress.Country,
                    State = request.ShippingAddress.State,
                    Street = request.ShippingAddress.Street,
                    PostalCode = request.ShippingAddress.PostalCode,
                },
                UserId = _actor.Id,
                TotalAmount = request.OrderItems.Sum(x => x.TotalPrice),
                OrderItems = list,
            };

            foreach(OrderItem item in list)
            {
                item.Order = o;
            }

            Context.Orders.Add(o);
            Context.OrderItems.AddRange(list);
            Context.SaveChanges();
        }
    } 
}
    
