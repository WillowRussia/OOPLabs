using CourseManagementSystem.Builders;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

namespace Lab1
{
    class Program
    {
        private static readonly UniversitySystem _system = UniversitySystem.Instance;
        
        private static readonly List<Teacher> _teachers = new List<Teacher>();
        private static readonly List<Student> _students = new List<Student>();

        private static int _courseIdCounter = 1;
        private static int _teacherIdCounter = 1;
        private static int _studentIdCounter = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("--- Система Управления Университетскими Курсам ИТМО ---");
            
            bool running = true;
            while (running)
            {
                ShowMenu();
                Console.Write("\n> Ваш выбор: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowAllCourses();
                        break;
                    case "2":
                        AddCourse();
                        break;
                    case "3":
                        AssignTeacherToCourse();
                        break;
                    case "4":
                        EnrollStudentInCourse();
                        break;
                    case "5":
                        ShowCoursesByTeacher();
                        break;
                    case "6":
                        AddTeacher();
                        break;
                    case "7":
                        AddStudent();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Выход из программы...");
                        break;
                    default:
                        Console.WriteLine("Ошибка: Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
                Pause();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- Главное Меню ---");
            Console.WriteLine("1. Показать все курсы");
            Console.WriteLine("2. Добавить новый курс");
            Console.WriteLine("3. Назначить преподавателя на курс");
            Console.WriteLine("4. Записать студента на курс");
            Console.WriteLine("5. Показать все курсы преподавателя");
            Console.WriteLine("--- Администрирование ---");
            Console.WriteLine("6. Добавить нового преподавателя");
            Console.WriteLine("7. Добавить нового студента");
            Console.WriteLine("0. Выход");
        }

        static void AddCourse()
        {
            Console.WriteLine("\n--- Добавление нового курса ---");
            Console.Write("Введите название курса: ");
            string name = Console.ReadLine() ?? "Без названия";

            Console.Write("Тип курса (1 - Онлайн, 2 - Офлайн): ");
            string type = Console.ReadLine() ?? "";

            Course newCourse;
            if (type == "1")
            {
                Console.Write("Введите платформу (например, Stepik): ");
                string platform = Console.ReadLine() ?? "Не указана";
                newCourse = new OnlineCourseBuilder()
                    .WithId(_courseIdCounter++)
                    .WithName(name)
                    .WithPlatform(platform)
                    .Build();
            }
            else if (type == "2")
            {
                Console.Write("Введите адрес: ");
                string address = Console.ReadLine() ?? "Не указан";
                Console.Write("Введите номер аудитории: ");
                string room = Console.ReadLine() ?? "N/A";
                newCourse = new OfflineCourseBuilder()
                    .WithId(_courseIdCounter++)
                    .WithName(name)
                    .WithAddress(address, room)
                    .Build();
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный тип курса.");
                return;
            }

            _system.AddCourse(newCourse);
            Console.WriteLine($"Успешно добавлен курс: {newCourse.GetCourseDetails()}");
        }

        static void AddTeacher()
        {
            Console.WriteLine("\n--- Добавление нового преподавателя ---");
            Console.Write("Введите имя преподавателя: ");
            string name = Console.ReadLine() ?? "Безымянный";
            
            var teacher = new Teacher { Id = _teacherIdCounter++, Name = name };
            _teachers.Add(teacher);
            
            Console.WriteLine($"Преподаватель '{name}' успешно добавлен с ID: {teacher.Id}");
        }

        static void AddStudent()
        {
            Console.WriteLine("\n--- Добавление нового студента ---");
            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine() ?? "Безымянный";
            
            var student = new Student { Id = _studentIdCounter++, Name = name };
            _students.Add(student);

            Console.WriteLine($"Студент '{name}' успешно добавлен с ID: {student.Id}");
        }

        static void ShowAllCourses()
        {
            Console.WriteLine("\n--- Список всех курсов ---");
            var allCourses = _system.AllCourses;
            if (!allCourses.Any())
            {
                Console.WriteLine("Курсов пока нет.");
                return;
            }

            foreach (var course in allCourses)
            {
                var teacherName = course.AssignedTeacher?.Name ?? "не назначен";
                Console.WriteLine($"ID: {course.Id} | {course.GetCourseDetails()} | Преподаватель: {teacherName}");
                if (course.EnrolledStudents.Any())
                {
                    Console.WriteLine($"  Студенты: {string.Join(", ", course.EnrolledStudents.Select(s => s.Name))}");
                }
            }
        }
        
        static void AssignTeacherToCourse()
        {
            Console.WriteLine("\n--- Назначение преподавателя ---");
            var teacher = SelectEntity(_teachers, "преподавателя");
            if (teacher == null) return;

            var course = SelectEntity(_system.AllCourses.ToList(), "курса");
            if (course == null) return;

            _system.AssignTeacherToCourse(teacher, course.Id);
            Console.WriteLine($"Преподаватель '{teacher.Name}' назначен на курс '{course.Name}'.");
        }

        static void EnrollStudentInCourse()
        {
            Console.WriteLine("\n--- Запись студента на курс ---");
            var student = SelectEntity(_students, "студента");
            if (student == null) return;

            var course = SelectEntity(_system.AllCourses.ToList(), "курса");
            if (course == null) return;

            _system.EnrollStudentInCourse(student, course.Id);
            Console.WriteLine($"Студент '{student.Name}' записан на курс '{course.Name}'.");
        }
        
        static void ShowCoursesByTeacher()
        {
            Console.WriteLine("\n--- Поиск курсов по преподавателю ---");
            var teacher = SelectEntity(_teachers, "преподавателя");
            if (teacher == null) return;

            var courses = _system.GetCoursesByTeacher(teacher.Id).ToList();
            Console.WriteLine($"\nКурсы, которые ведет {teacher.Name}:");
            if (!courses.Any())
            {
                Console.WriteLine("Этот преподаватель не ведет курсов.");
                return;
            }
            
            foreach (var course in courses)
            {
                Console.WriteLine($"- {course.Name}");
            }
        }

        private static T SelectEntity<T>(List<T> entities, string entityName) where T : class
        {
            if (!entities.Any())
            {
                Console.WriteLine($"Ошибка: Нет доступных {entityName}ов. Сначала добавьте их.");
                return null;
            }

            Console.WriteLine($"Выберите {entityName}а из списка:");
            for (int i = 0; i < entities.Count; i++)
            {
                var nameProperty = typeof(T).GetProperty("Name");
                var idProperty = typeof(T).GetProperty("Id");
                Console.WriteLine($"{i + 1}. ID: {idProperty?.GetValue(entities[i])}, Имя: {nameProperty?.GetValue(entities[i])}");
            }

            Console.Write("> Ваш выбор (номер): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= entities.Count)
            {
                return entities[index - 1];
            }

            Console.WriteLine("Ошибка: Неверный выбор.");
            return null;
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
            Console.ReadLine();
        }
    }
}