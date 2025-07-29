using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team28HackathonAPI.Models;

namespace team28HackathonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            if (request.Username == "testuser" && request.Password == "password123")
            {
                HttpContext.Session.SetString("User", request.Username);
                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized("Invalid username or password.");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

