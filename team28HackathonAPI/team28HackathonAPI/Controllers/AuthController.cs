using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using team28HackathonAPI.DBContext;
using team28HackathonAPI.Models;

namespace team28HackathonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //change to interface calls
        public readonly Team28DbContext _dbContext;
        public AuthController(Team28DbContext dbContext)
        {
            _dbContext = dbContext;
        }
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
        //will remove later
        [HttpGet]
        public IActionResult GetAlerts()
        {
            var alerts = _dbContext.Alerts.ToList();

            return Ok(alerts);
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

