
using Microsoft.AspNetCore.Mvc;
using webapi.services;

namespace webapi.Controller
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                var token = TokenService.GenerateToken(new models.Employee("admin", 32, null));
                return Ok(token);
            }

            return BadRequest("username or password is invalid");
        }
    }
}