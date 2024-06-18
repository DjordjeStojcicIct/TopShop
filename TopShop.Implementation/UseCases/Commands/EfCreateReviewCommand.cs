using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CommandDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using TopShop.Implementation.Validators;
using FluentValidation;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfCreateReviewCommand: EfUseCase, ICreateReviewCommand 
    {
        private readonly CreateReviewValidator _validator;
        private IApplicationActor _actor;
        public EfCreateReviewCommand(TopShopContext context, CreateReviewValidator v, IApplicationActor a) : base(context)
        {
            _validator = v;
            _actor = a;
        }

        public int Id => 23;

        public string Name => "Create Review";

        public void Execute(CreateReviewDTO request)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            Review r = new Review
            {
                UserId = _actor.Id,
                Comment = request.Comment,
                Rating = request.Rating,
                ProductId = request.ProductId,
            };
            Context.Reviews.Add(r);
            Context.SaveChanges();
        }
    }
}
