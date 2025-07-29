using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using team28HackathonAPI.DBContext;
using team28HackathonAPI.Models;

namespace team28HackathonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Team28DbContext _context;

        public AuthController(Team28DbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.AppUsers.FirstOrDefault(u =>
                u.Email == request.Email && u.Password == request.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return Ok(new { Message = "Login successful", user.Name });
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { Message = "Logged out" });
        }
    }
}

