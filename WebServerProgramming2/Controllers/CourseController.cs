using Microsoft.AspNetCore.Mvc;
using WebServerProgramming2.Models;

namespace WebServerProgramming2.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : Controller
    {
        [HttpGet("")]

        public IActionResult GetAllCourses()
        {
            List<Course> courses = Database.Courses.GetCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(string id)
        {
            Course? course = Database.Courses.GetCourse(id);

            return course is not null ? Ok(course) : BadRequest($"There is no course with id: {id}");
        }

        [HttpPost("")]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            bool isCourseExist = Database.Courses.GetCourses().Any(c => c.Id == course.Id);

            if (isCourseExist)
                return Conflict($"There is already a course with the same ID.");

            bool isClassroomExist = Database.Classrooms.GetClassrooms().Any(c => c.Id == course.Classroom);

            if(!isClassroomExist)
                return BadRequest($"There is no classroom with id: {course.Classroom}");

            Database.Courses.AddCourse(course);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse([FromRoute(Name = "id")] string courseId, [FromBody] CourseUpdateModel course)
        {
            Course? existingCourse = Database.Courses.GetCourse(courseId);
            
            if (existingCourse is null)
                return NotFound($"There is no course with id: {courseId}");

            bool isClassroomExist = Database.Courses.GetCourses().Any(c => c.Id == course.Classroom);

            if (!isClassroomExist)
                return BadRequest($"There is no course with id: {course.Classroom}");

            Database.Courses.UpdateCourse(course, courseId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(string id)
        {
            Course? course = Database.Courses.GetCourse(id);

            if (course is null)
                return NotFound($"There is no course with id: {id}");

            Database.Courses.DeleteCourse(course);

            return Ok();
        }
    }
}
