using TopShop.Application.UseCases.DTO;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TopShop.API.DTO;

namespace TopShop.API.Extensions
{
    public static class ValidationExceptions
    { 
        public static UnprocessableEntityObjectResult ToUnprocessableEntity(this ValidationResult result)
        {
            var errors = result.Errors.Select(x => new ClientErrorDTO
            {
                Error = x.ErrorMessage,
                Property = x.PropertyName
            });

            return new UnprocessableEntityObjectResult(errors);
        }
    }
}
