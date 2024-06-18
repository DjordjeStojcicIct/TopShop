using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopShop.Application.UseCaseHandling;
using TopShop.Application.UseCases.Commands;
using TopShop.Application.UseCases.DTO.CreateDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public UsersController(ICommandHandler commandHandler, IQueryHandler q)
        {
            _queryHandler = q;
            _commandHandler = commandHandler;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearchDTO dto, [FromServices] IGetUsersQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, dto));
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindUserQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDTO dto, [FromServices] IRegisterUserCommand c)
        {
            _commandHandler.HandleCommand(c,dto);
            return NoContent();
        }
    }
}
