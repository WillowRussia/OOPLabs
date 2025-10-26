namespace CourseManagementSystem.Models
{
    public class OnlineCourse : Course
    {
        public string Platform { get; set; }

        public override string GetCourseDetails()
        {
            return $"Онлайн курс: {Name} на {Platform}";
        }
    }
}