using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopShop.Application.UseCaseHandling;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CommandDTO;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistsController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public WishlistsController(ICommandHandler commandHandler, IQueryHandler q)
        {
            _queryHandler = q;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] WishlistSearchDTO dto, [FromServices] IGetWishlistsQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, dto));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindWishListQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateWishlistDTO dto, [FromServices] ICreateWishlistCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<WishlistsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteWishlistCommand c)
        {
            _commandHandler.HandleCommand(c, id);
            return NoContent();
        }
    }
}
