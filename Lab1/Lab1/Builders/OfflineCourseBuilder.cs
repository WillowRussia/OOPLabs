using CourseManagementSystem.Models;

namespace CourseManagementSystem.Builders
{
    public class OfflineCourseBuilder : CourseBuilder<OfflineCourseBuilder, OfflineCourse>
    {
        public OfflineCourseBuilder WithAddress(string address, string room)
        {
            _course.Address = address;
            _course.RoomNumber = room;
            return this;
        }
    }
}
