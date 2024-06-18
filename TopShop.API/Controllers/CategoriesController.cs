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
    public class CategoriesController : ControllerBase
    {
        // GET: api/<CategoriesController>
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public CategoriesController(ICommandHandler commandHandler, IQueryHandler q)
        {
            _queryHandler = q;
            _commandHandler = commandHandler;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] CategoriesSearchDTO dto, [FromServices] IGetCategoriesQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, dto));
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindCategoryQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDTO dto, [FromServices] ICreateCategoryCommand c)
        {
            _commandHandler.HandleCommand(c, dto);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditCategoryDTO dto, [FromServices] IEditCategoryCommand c)
        {
            _commandHandler.HandleCommand(c, dto);
            return NoContent();
        }
    }
}
