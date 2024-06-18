using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopShop.Application.UseCaseHandling;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;
using TopShop.DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public OrdersController(ICommandHandler commandHandler, IQueryHandler q)
        {
            _queryHandler = q;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearchDTO dto, [FromServices] IGetOrdersQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q,dto));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindOrderQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDTO dto, [FromServices] ICreateOrderCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteOrderCommand c)
        {
            _commandHandler.HandleCommand(c, id);
            return NoContent();
        }
    }
}
