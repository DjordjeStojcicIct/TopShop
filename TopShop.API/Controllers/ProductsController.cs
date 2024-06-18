using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TopShop.API.Extensions;
using TopShop.Implementation.Validators;
using TopShop.DataAccess;
using TopShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCaseHandling;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.Queries;
using TopShop.Application.UseCases.DTO.SearchDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private ICommandHandler _commandHandler;
        private IQueryHandler _queryHandler;

        public ProductsController(ICommandHandler handler, IQueryHandler queryHandler)
        {
            _commandHandler = handler;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearchDTO dto, [FromServices] IGetProductsQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, dto));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindProductQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateProductDTO dto, [FromServices] ICreateProductCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
