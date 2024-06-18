using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using TopShop.API.Jwt;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtManager _manager;

        public AuthController(JwtManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthRequestDTO dto, [FromServices] TopShopContext context)
        {
            string token = _manager.MakeToken(dto.Email, dto.Password);
            return Ok(new { token });
        }

        // DELETE api/<AuthController>/5
        [HttpDelete]
        [Authorize]
        public IActionResult InvalidToken([FromServices] ITokenStorage storage)
        {
            StringValues header = HttpContext.Request.Headers["Authorization"];
            string token = header.ToString().Split("Bearer ")[1];
            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken? tokenObj = handler.ReadJwtToken(token);
            string jti = tokenObj?.Claims?.FirstOrDefault(x => x.Type == "jti")?.Value??"";
            storage.InvalidateToken(jti);
            return NoContent();
        }
    }
}
