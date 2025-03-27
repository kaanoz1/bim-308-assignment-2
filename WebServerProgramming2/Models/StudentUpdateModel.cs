namespace WebServerProgramming2.Models
{
    public class StudentUpdateModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required List<string> Courses { get; set; }
    }
}
