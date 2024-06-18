using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Uploads;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using TopShop.Implementation.Validators;

namespace TopShop.Implementation.UseCases.Commands
{
    public class EfCreateProductCommand : EfUseCase, ICreateProductCommand
    {
        private readonly AddProductValidator _validator;
        private readonly IBase64FileUploader _fileUploader;

        public EfCreateProductCommand(TopShopContext context, AddProductValidator validator, IBase64FileUploader fileUploader) : base(context)
        {
            _validator = validator;
            _fileUploader = fileUploader;
        }

        public int Id => 3;

        public string Name => "Create Product";

        public void Execute(CreateProductDTO request)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            Category? c = Context.Categories.FirstOrDefault(x => x.Id == request.CategoryId);

            if (c == null)
            {
                throw new InvalidOperationException("Category doesn't exists.");
            }


            string filePath = "default.jpg";

            if (request.ProductImageFile != null)
            {
                filePath = _fileUploader.Upload(request.ProductImageFile, UploadType.Product);
            }

            Product p = new Product
            {
                Category = c,
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockCount,
                ProductImage = new FileT
                {
                    Path = filePath,
                    Size = 100,
                },
            };
            Context.Products.Add(p);
            Context.SaveChanges();
        }
    }
}
