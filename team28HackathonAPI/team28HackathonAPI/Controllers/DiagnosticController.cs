using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using team28HackathonAPI.DBContext;
using team28HackathonAPI.Models;

namespace team28HackathonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticController : ControllerBase
    {
        private readonly Team28DbContext _context;

        public DiagnosticController(Team28DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return Unauthorized("You must be logged in.");

            var tests = _context.DiagnosticTests.ToList();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return Unauthorized("You must be logged in.");

            var test = _context.DiagnosticTests.Find(id);
            if (test == null)
                return NotFound();

            return Ok(test);
        }

        [HttpPost]
        public IActionResult Add(DiagnosticTest test)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return Unauthorized("You must be logged in.");

            test.Date = DateTime.UtcNow; 
            _context.DiagnosticTests.Add(test);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = test.Id }, test);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DiagnosticTest updatedTest)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return Unauthorized("You must be logged in.");

            if (id != updatedTest.Id)
                return BadRequest("ID mismatch");

            var test = _context.DiagnosticTests.Find(id);
            if (test == null)
                return NotFound();

            test.Name = updatedTest.Name;
            test.Result = updatedTest.Result;
            test.Date = updatedTest.Date; 

            _context.Entry(test).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return Unauthorized("You must be logged in.");

            var test = _context.DiagnosticTests.Find(id);
            if (test == null)
                return NotFound();

            _context.DiagnosticTests.Remove(test);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
