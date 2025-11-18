using CourseManagementSystem.Builders;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

public class UniversitySystemTests
{
    [Fact]
    public void GetInstance_MultipleTimes_ReturnsSameInstance()
    {
        var system = UniversitySystem.Instance;
        system.Reset();

        var instance1 = UniversitySystem.Instance;
        var instance2 = UniversitySystem.Instance;

        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void AddCourse_ToSystem_IncreasesCourseCountAndAddsCourse()
    {
        var system = UniversitySystem.Instance;
        system.Reset();
        var course = new OnlineCourseBuilder()
            .WithId(1)
            .WithName("Основы C#")
            .Build();

        system.AddCourse(course);

        Assert.Single(system.AllCourses);
        Assert.Contains(course, system.AllCourses);
    }

    [Fact]
    public void RemoveCourse_FromSystemWithOneCourse_MakesCourseListEmpty()
    {
        var system = UniversitySystem.Instance;
        system.Reset();
        var course = new OfflineCourseBuilder()
            .WithId(1)
            .WithName("Продвинутый Java")
            .WithAddress("Кронверский", "49")
            .Build();
        system.AddCourse(course);

        system.RemoveCourse(1);

        Assert.Empty(system.AllCourses);
    }

    [Fact]
    public void AssignTeacher_ToCourse_CorrectlySetsTeacherForCourse()
    {
        var system = UniversitySystem.Instance;
        system.Reset();
        var teacher = new Teacher { Id = 101, Name = "Сергей Владимирович" };
        var course = new OnlineCourseBuilder()
            .WithId(1)
            .WithName("С#")
            .Build();
        system.AddCourse(course);

        system.AssignTeacherToCourse(teacher, 1);
        
        var storedCourse = system.AllCourses.First();
        Assert.NotNull(storedCourse.AssignedTeacher);
        Assert.Equal("Сергей Владимирович", storedCourse.AssignedTeacher.Name);
        Assert.Same(teacher, storedCourse.AssignedTeacher);
    }

    [Fact]
    public void EnrollStudent_InCourse_AddsStudentToCourseRoster()
    {
        var system = UniversitySystem.Instance;
        system.Reset();
        var student = new Student { Id = 1, Name = "Сергей Владимирович" };
        var course = new OfflineCourseBuilder()
            .WithId(1)
            .WithName("C#")
            .Build();
        system.AddCourse(course);

        system.EnrollStudentInCourse(student, 1);

        var storedCourse = system.AllCourses.First();
        Assert.Single(storedCourse.EnrolledStudents);
        Assert.Contains(student, storedCourse.EnrolledStudents);
    }

    [Fact]
    public void GetCourses_ByTeacherId_ReturnsOnlyCoursesAssignedToThatTeacher()
    {
        var system = UniversitySystem.Instance;
        system.Reset();
        var teacher1 = new Teacher { Id = 101, Name = "Сергей Владимирович" };
        var teacher2 = new Teacher { Id = 102, Name = "Николай Кочубеев" };

        var course1 = new OnlineCourseBuilder().WithId(1).WithName("C#").Build();
        var course2 = new OfflineCourseBuilder().WithId(2).WithName("Java").Build();
        var course3 = new OnlineCourseBuilder().WithId(3).WithName("Алгоритмы").Build();

        system.AddCourse(course1);
        system.AddCourse(course2);
        system.AddCourse(course3);
        
        system.AssignTeacherToCourse(teacher1, 1);
        system.AssignTeacherToCourse(teacher2, 2);
        system.AssignTeacherToCourse(teacher1, 3);
        
        var teacher1Courses = system.GetCoursesByTeacher(101).ToList();
        
        Assert.Equal(2, teacher1Courses.Count);
        Assert.Contains(teacher1Courses, c => c.Name == "C#");
        Assert.Contains(teacher1Courses, c => c.Name == "Алгоритмы");
        Assert.DoesNotContain(teacher1Courses, c => c.Name == "Java");
    }
}