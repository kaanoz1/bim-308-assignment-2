namespace WebServerProgramming2.Models
{
    public class Classroom
    {
        public required string Id { get; init; }
        public required string Description { get; set; }
        public required ulong Capacity { get; set; }
    }
}
