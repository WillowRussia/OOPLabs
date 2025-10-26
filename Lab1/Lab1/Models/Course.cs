namespace CourseManagementSystem.Models
{
    public abstract class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher AssignedTeacher { get; set; }
        private readonly List<Student> _enrolledStudents = new List<Student>();

        public IReadOnlyList<Student> EnrolledStudents => _enrolledStudents.AsReadOnly();

        public void EnrollStudent(Student student)
        {
            if (!_enrolledStudents.Contains(student))
            {
                _enrolledStudents.Add(student);
            }
        }

        public abstract string GetCourseDetails();
    }
}