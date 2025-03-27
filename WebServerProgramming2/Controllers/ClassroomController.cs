using Microsoft.AspNetCore.Mvc;
using WebServerProgramming2.Models;

namespace WebServerProgramming2.Controllers
{
    [ApiController]
    [Route("api/classrooms")]
    public class ClassroomController : Controller
    {

        [HttpGet("")]
        public IActionResult GetAllClassrooms()
        {
            List<Classroom> classrooms = Database.Classrooms.GetClassrooms();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassrooms(string id)
        {
            Classroom? classroom = Database.Classrooms.GetClassroom(id);

            return classroom is not null ? Ok(classroom) : BadRequest($"There is no classroom with id: {id}");
        }
    }
}
