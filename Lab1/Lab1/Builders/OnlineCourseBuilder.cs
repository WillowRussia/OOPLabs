using CourseManagementSystem.Models;

namespace CourseManagementSystem.Builders
{
    public class OnlineCourseBuilder : CourseBuilder<OnlineCourseBuilder, OnlineCourse>
    {
        public OnlineCourseBuilder WithPlatform(string platform)
        {
            _course.Platform = platform;
            return this;
        }
    }
}