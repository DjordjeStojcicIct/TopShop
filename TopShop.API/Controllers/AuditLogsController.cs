using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopShop.Application.UseCaseHandling;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;

        public AuditLogsController(ICommandHandler commandHandler, IQueryHandler q)
        {
            _queryHandler = q;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearchDTO dto, [FromServices] IGetAuditLogsQuery q)
        {
            return Ok(_queryHandler.HandleQuery(q, dto));
        }
    }
}
