using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopShop.Application.UseCaseHandling;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CommandDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public ReviewsController(ICommandHandler commandHandler, IQueryHandler q)
        {
            _queryHandler = q;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ReviewSearchDTO dto, [FromServices] IGetReviewsQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, dto));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindReviewQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateReviewDTO dto, [FromServices] ICreateReviewCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteReviewCommand c)
        {
            _commandHandler.HandleCommand(c, id);
            return NoContent();
        }
    }
}
