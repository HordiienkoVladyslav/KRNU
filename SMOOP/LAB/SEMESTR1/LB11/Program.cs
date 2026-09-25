using System;

namespace LB11
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Введіть кількість студентів у групі для тестування: ");
            int k = 0;

            while (!int.TryParse(Console.ReadLine(), out k) || k <= 0)
            {
                Console.Write("Некоректне введення. Введіть додатне число: ");
            }

            Group group = new Group(k);

            // Ініціалізація сервісів бізнес-логіки
            IStudentStatistics statsService = new StudentStatistics();
            IGroupAnalyzer analyzerService = new GroupAnalyzer(statsService);
            IScholarshipService scholarshipService = new ScholarshipService();

            // Автоматичне заповнення випадковими тестовими даними
            int i = 0;
            while (i < k)
            {
                Student student = new Student(i + 1, $"Студент ПІП {i + 1}", $"КВ-{100 + i}", "+380501234567");

                Random rnd = new Random(i);
                // Заповнюємо 4 оцінки предметами для тестування
                student.AddSubject(new Subject("Математика", rnd.Next(65, 101)));
                student.AddSubject(new Subject("Програмування", rnd.Next(65, 101)));
                student.AddSubject(new Subject("Алгоритми", rnd.Next(65, 101)));
                student.AddSubject(new Subject("Фізика", rnd.Next(65, 101)));

                group.AddStudent(student);
                i++;
            }

            // Створення та запуск об'єкта меню
            MenuHandler menu = new MenuHandler(group, statsService, analyzerService, scholarshipService);
            menu.Start();
        }
    }
}