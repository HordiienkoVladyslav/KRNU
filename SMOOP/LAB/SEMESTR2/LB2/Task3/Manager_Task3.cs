using System;
using System.Collections.Generic;
using System.Text;

namespace LB2
{
    public static class Manager_Task3
    {
        public static void Run()
        {
            var company = new Company_Task3
            {
                Name = "Tech Solutions",
                Employees = new List<Employer>
                {
                    new President { FirstName = "Володимир", LastName = "Великий", BirthDate = new DateTime(1975, 5, 12), Salary = 50000, WorkExperienceYears = 25, HasHigherEducation = true },
                    new Manager { FirstName = "Олексій", LastName = "Петров", BirthDate = new DateTime(1990, 10, 5), Salary = 25000, WorkExperienceYears = 10, HasHigherEducation = true },
                    new Manager { FirstName = "Олена", LastName = "Сидорова", BirthDate = new DateTime(1982, 3, 15), Salary = 30000, WorkExperienceYears = 18, HasHigherEducation = true },
                    new Worker { FirstName = "Володимир", LastName = "Іванов", BirthDate = new DateTime(1998, 10, 20), Salary = 15000, WorkExperienceYears = 5, HasHigherEducation = true },
                    new Worker { FirstName = "Сергій", LastName = "Коваленко", BirthDate = new DateTime(2001, 1, 10), Salary = 12000, WorkExperienceYears = 2, HasHigherEducation = false },
                    new Worker { FirstName = "Володимир", LastName = "Бойко", BirthDate = new DateTime(2003, 7, 18), Salary = 11000, WorkExperienceYears = 1, HasHigherEducation = false },
                    new Worker { FirstName = "Андрій", LastName = "Ткаченко", BirthDate = new DateTime(1985, 10, 30), Salary = 18000, WorkExperienceYears = 15, HasHigherEducation = true },
                    new Worker { FirstName = "Максим", LastName = "Кравченко", BirthDate = new DateTime(1992, 11, 11), Salary = 16000, WorkExperienceYears = 8, HasHigherEducation = true },
                    new Worker { FirstName = "Дмитро", LastName = "Шевченко", BirthDate = new DateTime(1980, 4, 22), Salary = 20000, WorkExperienceYears = 20, HasHigherEducation = true },
                    new Worker { FirstName = "Ігор", LastName = "Зайцев", BirthDate = new DateTime(1988, 10, 2), Salary = 17000, WorkExperienceYears = 12, HasHigherEducation = true },
                    new Worker { FirstName = "Артем", LastName = "Мороз", BirthDate = new DateTime(1995, 6, 8), Salary = 14000, WorkExperienceYears = 6, HasHigherEducation = true }
                }
            };

            // 1. Кількість робітників (усіх працівників підприємства)
            int totalEmployees = company.Employees.Count;

            // 2. Об'єм заробітної платні, що необхідно виплатити
            decimal totalSalaryBudget = company.Employees.Sum(e => e.Salary);

            // 3. 10 робітників із найбільшим стажем -> наймолодший за віком із вищою освітою
            var youngestWithEduFromTop10Exp = company.Employees
                .OrderByDescending(e => e.WorkExperienceYears)
                .Take(10)
                .Where(e => e.HasHigherEducation)
                .OrderBy(e => e.Age)
                .FirstOrDefault();


            // 4. Наймолодший та найстарший менеджер компанії
            var managers = company.Employees.OfType<Manager>();
            var youngestManager = managers.OrderBy(m => m.Age).FirstOrDefault();
            var oldestManager = managers.OrderByDescending(m => m.Age).FirstOrDefault();



            // 5. Працівники, що народилися у жовтні, згруповані за професійним спрямуванням
            var octoberBornGrouped = company.Employees
                .Where(e => e.BirthDate.Month == 10)
                .GroupBy(e => e.GetType().Name);

            Console.WriteLine("\n5. Працівники, які народилися у жовтні (за посадами):");
            foreach (var group in octoberBornGrouped)
            {
                Console.WriteLine($"   -- {group.Key} --");
                foreach (var emp in group)
                {
                    Console.WriteLine($"      {emp.FirstName} {emp.LastName} ({emp.BirthDate:dd.MM.yyyy})");
                }
            }

            // 6. Усі Володимири -> Обрати наймолодшого та нарахувати премію (1/3 окладу)
            var allVolodymyrs = company.Employees
                .Where(e => e.FirstName.Equals("Володимир", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var youngestVolodymyr = allVolodymyrs.OrderBy(v => v.Age).FirstOrDefault();

            Console.WriteLine($"\n6. Знайдено Володимирів: {allVolodymyrs.Count}");
            if (youngestVolodymyr != null)
            {
                decimal bonus = youngestVolodymyr.Salary / 3;
                Console.WriteLine($"   Вітаємо співробітника: {youngestVolodymyr.FirstName} {youngestVolodymyr.LastName}!");
                Console.WriteLine($"   Вам призначено премію у розмірі 1/3 окладу: {bonus:F2} грн.");
            }
        }
    }
}
