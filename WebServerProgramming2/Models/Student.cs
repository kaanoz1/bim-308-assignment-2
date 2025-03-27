namespace WebServerProgramming2.Models
{
    public class Student
    {
        public required ulong Id { get; init; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required List<string> Courses { get; set; }
    }

}
