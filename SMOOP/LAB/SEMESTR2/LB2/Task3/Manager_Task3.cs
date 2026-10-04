using System;
using System.Collections.Generic;
using System.Linq;

namespace LB2
{
    public static class Manager_Task3
    {
        public static void Run()
        {
            Company company = new Company("Tech Solutions Ukraine");

            company.Employees.AddRange(new List<Employer>
        {
            new President("Володимир", "Петренко", new DateTime(1975, 5, 12), 100000, 25, true),
            
            new Manager("Олена", "Ковальчук", new DateTime(1995, 10, 3), 45000, 5, true),
            new Manager("Олександр", "Григоренко", new DateTime(1982, 3, 19), 55000, 18, true),
            new Manager("Володимир", "Сидоренко", new DateTime(1999, 10, 25), 38000, 3, true),

            new Worker("Володимир", "Бойко", new DateTime(2001, 7, 14), 25000, 2, true),
            new Worker("Андрій", "Мельник", new DateTime(1980, 10, 8), 30000, 20, false),
            new Worker("Дмитро", "Шевченко", new DateTime(1985, 12, 1), 32000, 15, true),
            new Worker("Ігор", "Ткаченко", new DateTime(1978, 4, 22), 35000, 22, true),
            new Worker("Степан", "Кравченко", new DateTime(1990, 10, 11), 28000, 10, false),
            new Worker("Сергій", "Мороз", new DateTime(1983, 2, 17), 31000, 16, true),
            new Worker("Володимир", "Василенко", new DateTime(1992, 9, 30), 29000, 8, true),
            new Worker("Михайло", "Павлюк", new DateTime(1972, 6, 5), 36000, 28, true),
            new Worker("Олег", "Савченко", new DateTime(1988, 10, 15), 27000, 12, true),
            new Worker("Максим", "Руденко", new DateTime(1994, 1, 20), 26000, 7, false)
        });

            Console.WriteLine($"=== ПІДПРИЄМСТВО: {company.Name} ===\n");

            // 1. Кількість робітників підприємства (лише екземпляри класу Worker)
            int workerCount = company.Employees.OfType<Worker>().Count();
            Console.WriteLine($"1. Загальна кількість робітників: {workerCount}");

            // 2. Загальний об'єм заробітної платні для виплати всім робітникам
            double totalSalary = company.Employees.Sum(e => e.Salary);
            Console.WriteLine($"2. Загальний фонд заробітної плати: {totalSalary:C}\n");

            // 3. Топ-10 за стажем -> наймолодший з них із вищою освітою
            Console.WriteLine("3. Наймолодший працівник з вищою освітою серед 10-ти з найбільшим стажем:");
            var top10ByExperience = company.Employees
                .OrderByDescending(e => e.WorkExperienceYears)
                .Take(10)
                .ToList();

            var youngestWithHigherEdu = top10ByExperience
                .Where(e => e.HasHigherEducation)
                .OrderBy(e => e.Age)
                .First();

            if (youngestWithHigherEdu != null)
                Console.WriteLine($"   -> {youngestWithHigherEdu}\n");
            else
                Console.WriteLine("   -> Працівників з вищою освітою у даній вибірці не знайдено.\n");

            // 4. Наймолодший та найстарший менеджер
            var managers = company.Employees.OfType<Manager>().ToList();
            if (managers.Any())
            {
                var youngestManager = managers.OrderBy(m => m.Age).First();
                var oldestManager = managers.OrderByDescending(m => m.Age).First();

                Console.WriteLine("4. Менеджери:");
                Console.WriteLine($"   • Наймолодший: {youngestManager}");
                Console.WriteLine($"   • Найстарший:  {oldestManager}\n");
            }

            // 5. Працівники, що народилися у жовтні (згруповані за посадою)
            Console.WriteLine("5. Співробітники, які народилися у жовтні (за професійним спрямуванням):");
            var octoberBornGrouped = company.Employees
                .Where(e => e.BirthDate.Month == 10)
                .GroupBy(e => e.GetType().Name);

            foreach (var group in octoberBornGrouped)
            {
                Console.WriteLine($"   Категорія [{group.Key}]:");
                foreach (var emp in group)
                {
                    Console.WriteLine($"     - {emp.FirstName} {emp.LastName} (Дата народження: {emp.BirthDate:dd.MM.yyyy})");
                }
            }
            Console.WriteLine();

            // 6. Усі Володимири -> привітання наймолодшого з премією (1/3 окладу)
            Console.WriteLine("6. Усі працівники з ім'ям Володимир:");
            var vladimirs = company.Employees
                .Where(e => e.FirstName.Equals("Володимир", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var v in vladimirs)
            {
                Console.WriteLine($"   - {v}");
            }

            var youngestVladimir = vladimirs.OrderBy(v => v.Age).First();
            if (youngestVladimir != null)
            {
                double bonus = youngestVladimir.Salary / 3.0;
                Console.WriteLine($"\n   🎉 ПОЗДОРОВЛЕННЯ!");
                Console.WriteLine($"   Наймолодший Володимир — {youngestVladimir.FirstName} {youngestVladimir.LastName} ({youngestVladimir.Age} р.).");
                Console.WriteLine($"   Вітаємо з отриманням премії у розмірі {bonus:C} (1/3 від окладу {youngestVladimir.Salary:C})!");
            }
        }
    }
}