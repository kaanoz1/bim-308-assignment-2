using Microsoft.AspNetCore.Mvc;
using WebServerProgramming2.Models;

namespace WebServerProgramming2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : Controller
    {
        [HttpGet("")]
        public IActionResult GetAllStudents()
        {
            List<Student> students = Database.Students.GetStudents();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(ulong id)
        {
            Student? student = Database.Students.GetStudent(id);

            return student is not null? Ok(student) : BadRequest($"There is no student with id: {id}");
        }

        [HttpPost("")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
           bool isStudentExist = Database.Students.GetStudents().Any(s => s.Id == student.Id || s.Email == student.Email);

            if (isStudentExist)
                return Conflict($"There is a already a student whom has same Id or Email.");

            bool isCoursesExist = student.Courses.All(c => Database.Courses.GetCourses().Any(course => course.Id == c));

            if (!isCoursesExist)
            {
                string coursesDoesNotExist = string.Join(", ", student.Courses.Where(c => !Database.Courses.GetCourses().Any(course => course.Id == c)));
                return BadRequest($"There is an incompatibility the courses we have and you offered to be exist: {coursesDoesNotExist}");
            }
            Database.Students.AddStudent(student);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute(Name= "id")] ulong studentId,[FromBody] StudentUpdateModel student)
        {
            Student? existingStudent = Database.Students.GetStudent(studentId);

            if (existingStudent is null)
                return NotFound($"There is no student with id: {studentId}");

            bool isCoursesExist = student.Courses.All(c => Database.Courses.GetCourses().Any(course => course.Id == c));

            if (!isCoursesExist)
            {
                string coursesDoesNotExist = string.Join(", ", student.Courses.Where(c => !Database.Courses.GetCourses().Any(course => course.Id == c)));
                return BadRequest($"There is an incompatibility the courses we have and you offered to be exist: {coursesDoesNotExist}");
            }

            Database.Students.UpdateStudent(student, studentId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(ulong id)
        {
            Student? student = Database.Students.GetStudent(id);

            if (student is null)
                return NotFound($"There is no student with id: {id}");

            Database.Students.DeleteStudent(student);

            return Ok();
        }

    }
}
