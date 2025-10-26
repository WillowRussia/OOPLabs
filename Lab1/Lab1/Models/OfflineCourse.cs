namespace CourseManagementSystem.Models
{
    public class OfflineCourse : Course
    {
        public string Address { get; set; }
        public string RoomNumber { get; set; }

        public override string GetCourseDetails()
        {
            return $"Оффлайн курс: {Name} на {Address}, кабинет: {RoomNumber}";
        }
    }
}