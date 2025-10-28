using CourseManagementSystem.Models;

namespace CourseManagementSystem.Services
{
    public sealed class UniversitySystem
    {
        private static readonly UniversitySystem _instance = new UniversitySystem();
        private readonly List<Course> _courses = new List<Course>();

        private UniversitySystem() { }

        public static UniversitySystem Instance => _instance;

        public void AddCourse(Course course)
        {
            if (!_courses.Contains(course))
            {
                _courses.Add(course);
            }
        }

        public void RemoveCourse(int courseId)
        {
            var courseToRemove = _courses.FirstOrDefault(c => c.Id == courseId);
            if (courseToRemove != null)
            {
                _courses.Remove(courseToRemove);
            }
        }

        public void AssignTeacherToCourse(Teacher teacher, int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                course.AssignedTeacher = teacher;
            }
        }

        public void EnrollStudentInCourse(Student student, int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            course?.EnrollStudent(student);
        }

        public IEnumerable<Course> GetCoursesByTeacher(int teacherId)
        {
            return _courses.Where(c => c.AssignedTeacher?.Id == teacherId);
        }
        
        public IReadOnlyList<Course> AllCourses => _courses.AsReadOnly();

        public void Reset()
        {
            _courses.Clear();
        }
    }
}