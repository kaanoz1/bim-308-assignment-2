using WebServerProgramming2.Models;

namespace WebServerProgramming2.Database
{
    public static class Students
    {

        static readonly List<Student> students = [
            new() {Id= 1, Name= "Example Student", Courses = ["BIM308", "BIM218", "BIM459"], Email= "exampleemail@example.com"},
            new() {Id= 44488883333, Name= "Emin Talip Demirkiran", Courses = ["BIM308", "BIM304"], Email= "exampleemail1@example.com"},
            ];

        public static List<Student> GetStudents() => students;
        public static Student? GetStudent(ulong id) => students.FirstOrDefault(s => s.Id == id);
        public static void AddStudent(Student student) => students.Add(student);
        public static void UpdateStudent(StudentUpdateModel student, ulong studentId)
        {
            int existingStudentIndex = students.FindIndex(s => s.Id == studentId);

            if(existingStudentIndex is -1)
                throw new ArgumentNullException($"There is no student with id: {studentId}");

            Student? existingStudent = students[existingStudentIndex] ?? throw new InvalidOperationException($"Student found at index {existingStudentIndex} is unexpectedly null.");

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Courses = student.Courses;

        }
        public static void DeleteStudent(Student student)
        {
            int existingStudentIndex = students.FindIndex(s => s.Id == student.Id);

            if (existingStudentIndex is -1)
                throw new ArgumentNullException($"There is no student with id: {student.Id}");

            Student? existingStudent = students[existingStudentIndex] ?? throw new InvalidOperationException($"Student found at index {existingStudentIndex} is unexpectedly null.");
            
            students.Remove(existingStudent);

        }
    }

    public static class Classrooms
    {
        static readonly List<Classroom> classrooms = [
            new() {Id = "B6", Description = "Computer Engineering First Floor", Capacity = 95 },
            new() {Id = "B7", Description = "Computer Engineering Second Floor", Capacity = 76 },
            new() {Id = "B1", Description = "Computer Engineering First Floor, at backside of the B6", Capacity = 57 },
        ];
        public static List<Classroom> GetClassrooms() => classrooms;
        public static Classroom? GetClassroom(string id) => classrooms.FirstOrDefault(s => s.Id == id);

    }
    public static class Courses
    {

        static readonly List<Course> courses = [
         new () {Id = "BIM308", Title = "Web Server Programming",Classroom = "B6"},
            new () {Id = "BIM218", Title = "Operating Systems",Classroom = "B7"},
            new () {Id = "BIM459", Title = "Artificial Intelligence in Healtcare",Classroom = "B6"},
            new () {Id = "BIM447", Title = "Introduction to Deep Learning",Classroom = "B6"},
            new () {Id = "BIM304", Title = "Computer Algorithm and Design",Classroom = "B6"},
            new () {Id = "BIM324", Title = "Computer Networks",Classroom = "B6"},
            new () {Id = "BIM496", Title = "Computer Vision",Classroom = "B1"},
            new () {Id = "BIM492", Title = "Design Patterns",Classroom = "B6"},
            ];


        public static List<Course> GetCourses() => courses;
        public static Course? GetCourse(string id) => courses.FirstOrDefault(s => s.Id == id);
        public static void AddCourse(Course course) => courses.Add(course);


        public static void UpdateCourse(CourseUpdateModel course, string courseId)
        {
            int existingCourseIndex = courses.FindIndex(c => c.Id == courseId);

            if (existingCourseIndex is -1)
                throw new ArgumentNullException($"There is no course with id: {courseId}");

            Course? existingCourse = courses[existingCourseIndex] ?? throw new InvalidOperationException($"Student found at index {existingCourseIndex} is unexpectedly null.");

            existingCourse.Title = course.Title;
            existingCourse.Classroom = course.Classroom;

        }
        public static void DeleteCourse(Course course)
        {
            int existingStudentIndex = courses.FindIndex(c => c.Id == course.Id);

            if (existingStudentIndex is -1)
                throw new ArgumentNullException($"There is no course with id: {course.Id}");

            Course? existingCourse = courses[existingStudentIndex] ?? throw new InvalidOperationException($"Course found at index {existingStudentIndex} is unexpectedly null.");

            foreach(Student student in Students.GetStudents())
                student.Courses.Remove(course.Id);

            courses.Remove(existingCourse);

        }
    }


}
