using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Implementation.Validators;

namespace TopShop.Implementation.Extensions
{
    public static class ValidatorExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<AddProductValidator>();
            services.AddTransient<AddressValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateOrderItemValidator>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<CreateReviewValidator>();
            services.AddTransient<CreateWishlistValidator>();
        }
    }
    
}
