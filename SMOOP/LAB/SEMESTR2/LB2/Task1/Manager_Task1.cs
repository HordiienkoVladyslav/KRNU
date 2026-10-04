using System;
using System.Collections.Generic;
using System.Text;

namespace LB2
{
    public static class Manager_Task1
    {
            public static void Run()
            {
                var companies = new List<Company_Task1>
            {
                new Company_Task1 (  "Food",  new DateTime(2020, 1, 15),  "Food",  "John White",  150,  "London" ),
                new Company_Task1 (  "IT Masters", DateTime.Now.AddDays(-100),  "IT",  "Alex Black",  250,  "Kyiv" ),
                new Company_Task1 (  "White Soft",  new DateTime(2018, 5, 20), "IT",  "James Black",  80,  "London" ),
                new Company_Task1 (  "MarketPlus", new DateTime(2023, 11, 10),  "Marketing",  "Sarah White",  310,  "New York" ),
                new Company_Task1 (  "Fast Food Corp",  DateTime.Now.AddDays(-200),  "Marketing", "Tom Smith",  50,  "London" )
            };

            // 1. Інформація про всі фірми
            companies.ForEach(c => Console.WriteLine(c));

            // 2. Фірми, які мають назву Food
            companies
                .Where(c => c.Name.Equals("Food", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(c => Console.WriteLine(c));

            // 3. Фірми у галузі маркетингу
            companies.Where(c => c.BusinessProfile.Equals("Marketing", StringComparison.OrdinalIgnoreCase))
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 4. Фірми у галузі маркетингу або IT
            var name = "Marketing";
            companies.Where(c => c.BusinessProfile.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                                 c.BusinessProfile.Equals("IT", StringComparison.OrdinalIgnoreCase))
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 5. Кількість співробітників > 100
            companies.Where(c => c.EmployeesCount > 100)
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 6. Кількість співробітників у діапазоні від 100 до 300
            companies.Where(c => c.EmployeesCount >= 100 && c.EmployeesCount <= 300)
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 7. Фірми у Лондоні
            var name1 = companies.Where(c => c.Address.Contains("London", StringComparison.OrdinalIgnoreCase));
                     name1.ToList().ForEach(c => Console.WriteLine(c)); 

            // 8. Прізвище директора White
            companies.Where(c => c.DirectorFullName.EndsWith("White", StringComparison.OrdinalIgnoreCase))
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 9. Засновані понад 2 роки тому
            companies.Where(c => c.FoundationDate <= DateTime.Now.AddYears(-2))
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 10. З дня заснування минуло більше 150 днів
            companies.Where(c => (DateTime.Now - c.FoundationDate).TotalDays > 150)
                     .ToList().ForEach(c => Console.WriteLine(c));

            // 11. Прізвище директора Black та назва містить слово White
            companies.Where(c => c.DirectorFullName.EndsWith("Black", StringComparison.OrdinalIgnoreCase) &&
                                 c.Name.Contains("White", StringComparison.OrdinalIgnoreCase))
                     .ToList().ForEach(c => Console.WriteLine(c));
        }
    }
}

