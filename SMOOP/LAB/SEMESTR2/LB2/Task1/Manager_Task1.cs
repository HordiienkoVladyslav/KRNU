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
                new Company_Task1 { Name = "Food", FoundationDate = new DateTime(2020, 1, 15), BusinessProfile = "Food", DirectorFullName = "John White", EmployeesCount = 150, Address = "London" },
                new Company_Task1 { Name = "IT Masters", FoundationDate = DateTime.Now.AddDays(-100), BusinessProfile = "IT", DirectorFullName = "Alex Black", EmployeesCount = 250, Address = "Kyiv" },
                new Company_Task1 { Name = "White Soft", FoundationDate = new DateTime(2018, 5, 20), BusinessProfile = "IT", DirectorFullName = "James Black", EmployeesCount = 80, Address = "London" },
                new Company_Task1 { Name = "MarketPlus", FoundationDate = new DateTime(2023, 11, 10), BusinessProfile = "Marketing", DirectorFullName = "Sarah White", EmployeesCount = 310, Address = "New York" },
                new Company_Task1 { Name = "Fast Food Corp", FoundationDate = DateTime.Now.AddDays(-200), BusinessProfile = "Marketing", DirectorFullName = "Tom Smith", EmployeesCount = 50, Address = "London" }
            };

            // 1. Інформація про всі фірми
            var all_companies = companies;

            // 2. Фірми, які мають назву Food
            var food_companies = companies.Where(c => c.Name.Equals("Food", StringComparison.OrdinalIgnoreCase));

            // 3. Фірми у галузі маркетингу
            var market_companies = companies.Where(c => c.BusinessProfile.Equals("Marketing", StringComparison.OrdinalIgnoreCase));

            // 4. Фірми у галузі маркетингу або IT
            var market_it_companies = companies.Where(c => c.BusinessProfile.Equals("Marketing", StringComparison.OrdinalIgnoreCase) ||
                                          c.BusinessProfile.Equals("IT", StringComparison.OrdinalIgnoreCase));

            // 5. Кількість співробітників > 100
            var count_over_100 = companies.Where(c => c.EmployeesCount > 100);

            // 6. Кількість співробітників у діапазоні від 100 до 300
            var count_range = companies.Where(c => c.EmployeesCount >= 100 && c.EmployeesCount <= 300);

            // 7. Фірми у Лондоні
            var london_companies = companies.Where(c => c.Address.Contains("London", StringComparison.OrdinalIgnoreCase));

            // 8. Прізвище директора White
            var director_white = companies.Where(c => c.DirectorFullName.EndsWith("White", StringComparison.OrdinalIgnoreCase));

            // 9. Засновані понад 2 роки тому
            var more_than_2_years = companies.Where(c => c.FoundationDate <= DateTime.Now.AddYears(-2));

            // 10.З дня заснування минуло більше 150 днів
            var more_than_150_days = companies.Where(c => (DateTime.Now - c.FoundationDate).TotalDays > 150);

            // 11. Прізвище директора Black та назва містить слово White
            var director_black_name_white = companies.Where(c => c.DirectorFullName.EndsWith("Black", StringComparison.OrdinalIgnoreCase) &&
                                           c.Name.Contains("White", StringComparison.OrdinalIgnoreCase));
            }
        }
    }

