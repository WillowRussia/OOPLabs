using CourseManagementSystem.Builders;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

namespace CourseManagementSystem.Tests
{
    public class UniversitySystemTests
    {
        private readonly UniversitySystem _system;

        public UniversitySystemTests()
        {
            _system = UniversitySystem.Instance;
            _system.Reset(); 
        }

        [Fact]
        public void UniversitySystem_IsSingleton()
        {
            var instance1 = UniversitySystem.Instance;
            var instance2 = UniversitySystem.Instance;
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void AddCourse_ShouldAddCourseToSystem()
        {
            var course = new OnlineCourseBuilder()
                .WithId(1)
                .WithName("Основы C#")
                .WithPlatform("Unity")
                .Build();

            _system.AddCourse(course);

            Assert.Single(_system.AllCourses);
            Assert.Equal("Основы C#", _system.AllCourses.First().Name);
        }

        [Fact]
        public void RemoveCourse_ShouldRemoveCourseFromSystem()
        {
            // Проверяем, что курс корректно удаляется
            var course = new OfflineCourseBuilder()
                .WithId(1)
                .WithName("Продвинутый Java")
                .WithAddress("Кронверский", "49")
                .Build();

            _system.AddCourse(course);
            Assert.Single(_system.AllCourses);

            _system.RemoveCourse(1);

            Assert.Empty(_system.AllCourses);
        }

        [Fact]
        public void AssignTeacherToCourse_ShouldSetTeacher()
        {
            var teacher = new Teacher { Id = 101, Name = "Сергей Владимирович" };
            var course = new OnlineCourseBuilder()
                .WithId(1)
                .WithName("С#")
                .WithPlatform("Notion")
                .Build();

            _system.AddCourse(course);
            _system.AssignTeacherToCourse(teacher, 1);

            var storedCourse = _system.AllCourses.First();
            Assert.NotNull(storedCourse.AssignedTeacher);
            Assert.Equal("Сергей Владимирович", storedCourse.AssignedTeacher.Name);
        }

        [Fact]
        public void EnrollStudentInCourse_ShouldAddStudentToCourse()
        {
            // Проверяем запись студента на курс
            var student = new Student { Id = 1, Name = "Сергей Владимирович" };
            var course = new OfflineCourseBuilder()
                .WithId(1)
                .WithName("C#")
                .WithAddress("Кронверский", "49")
                .Build();

            _system.AddCourse(course);
            _system.EnrollStudentInCourse(student, 1);

            var storedCourse = _system.AllCourses.First();
            Assert.Single(storedCourse.EnrolledStudents);
            Assert.Equal("Сергей Владимирович", storedCourse.EnrolledStudents.First().Name);
        }

        [Fact]
        public void GetCoursesByTeacher_ShouldReturnCorrectCourses()
        {
            var teacher1 = new Teacher { Id = 101, Name = "Сергей Владимирович" };
            var teacher2 = new Teacher { Id = 102, Name = "Николай Кочубеев" };

            var course1 = new OnlineCourseBuilder().WithId(1).WithName("C#").Build();
            var course2 = new OfflineCourseBuilder().WithId(2).WithName("Java").Build();
            var course3 = new OnlineCourseBuilder().WithId(3).WithName("Алгоритмы").Build();

            _system.AddCourse(course1);
            _system.AddCourse(course2);
            _system.AddCourse(course3);

            _system.AssignTeacherToCourse(teacher1, 1);
            _system.AssignTeacherToCourse(teacher2, 2);
            _system.AssignTeacherToCourse(teacher1, 3);

            var teacher1Courses = _system.GetCoursesByTeacher(101).ToList();

            Assert.Equal(2, teacher1Courses.Count);
            Assert.Contains(teacher1Courses, c => c.Name == "C#");
            Assert.Contains(teacher1Courses, c => c.Name == "Алгоритмы");
        }
    }
}