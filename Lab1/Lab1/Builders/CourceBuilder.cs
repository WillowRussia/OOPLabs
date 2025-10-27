using CourseManagementSystem.Models;

namespace CourseManagementSystem.Builders
{
        public abstract class CourseBuilder<TBuilder, TCourse>
        where TBuilder : CourseBuilder<TBuilder, TCourse> // Гарантирует, что TBuilder - это мы сами или наш наследник
        where TCourse : Course, new() // Гарантирует, что TCourse - это курс, который можно создать
    {
        protected TCourse _course = new TCourse();

        public TBuilder WithId(int id)
        {
            _course.Id = id;
            return (TBuilder)this;
        }

        public TBuilder WithName(string name)
        {
            _course.Name = name;
            return (TBuilder)this;
        }

        public TBuilder WithTeacher(Teacher teacher)
        {
            _course.AssignedTeacher = teacher;
            return (TBuilder)this;
        }

        public Course Build()
        {
            return _course;
        }
    }
}