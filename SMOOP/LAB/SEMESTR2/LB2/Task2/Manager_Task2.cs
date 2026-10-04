using System;
using System.Collections.Generic;
using System.Text;

namespace LB2
{
    public static class Manager_Task2
    {
        public static void Run()
        {
            var phones = new List<Phone>
            {
                new Phone (  "iPhone 16",  "Apple",  2000,  new DateTime(2021, 9, 24) ),
                new Phone (  "iPhone 10",  "Apple",  450,  new DateTime(2017, 11, 3) ),
                new Phone (  "Galaxy S22",  "Samsung",  750,  new DateTime(2022, 2, 25) ),
                new Phone (  "Galaxy A52",  "Samsung",  350,  new DateTime(2021, 3, 17) ),
                new Phone (  "POCO X5 Pro",  "Xiaomi", 250,  new DateTime(2019, 5, 30) ),
                new Phone (  "Redmi Note 10", "Xiaomi",  150,  new DateTime(2021, 3, 16) ),
                new Phone (  "iPhone 13",  "Apple",  800,  new DateTime(2021, 9, 24) )
            };

            //// 1. Загальна кількість телефонів
            //int totalCount = phones.Count;

            //// 2. Кількість телефонів із ціною > 100
            //int countPriceOver100 = phones.Count(p => p.Price > 100);

            //// 3. Кількість телефонів із ціною від 400 до 700
            //int countPrice400To700 = phones.Count(p => p.Price >= 400 && p.Price <= 700);

            //// 4. Кількість телефонів конкретного виробника (наприклад, Apple)
            //int countApple = phones.Count(p => p.Manufacturer.Equals("Apple", StringComparison.OrdinalIgnoreCase));

            //// 5. Телефон із мінімальною ціною
            //var minPricePhone = phones.OrderBy(p => p.Price).FirstOrDefault();

            //// 6. Телефон із максимальною ціною
            //var maxPricePhone = phones.OrderByDescending(p => p.Price).FirstOrDefault();

            //// 7. Найстаріший телефон
            //var oldestPhone = phones.OrderBy(p => p.ReleaseDate).FirstOrDefault();

            //// 8. Найсвіжіший телефон
            //var newestPhone = phones.OrderByDescending(p => p.ReleaseDate).FirstOrDefault();

            //// 9. Середня ціна телефону
            //decimal avgPrice = phones.Average(p => p.Price);

            // 10. 5 найдорожчих телефонів
            //var top5Expensive = 
                phones.OrderByDescending(p => p.Price).Take(15)
                .ToList()
                .ForEach(p => Console.WriteLine(p));
            

            // 11. 5 найдешевших телефонів
            var top5Cheapest = phones.OrderBy(p => p.Price).Take(5);

            // 12. 3 найстаріші телефони
            var top3Oldest = phones.OrderBy(p => p.ReleaseDate).Take(3);

            // 13. 3 найновіші телефони
            var top3Newest = phones.OrderByDescending(p => p.ReleaseDate).Take(3);

            // 14. Статистика щодо кількості телефонів кожного виробника
            var statsByManufacturer = phones
                .GroupBy(p => p.Manufacturer)
                .Select(g => new { Manufacturer = g.Key, Count = g.Count() });

            // 15. Статистика щодо кількості моделей телефонів
            var statsByModel = phones
                .GroupBy(p => p.Name)
                .Select(g => new { Model = g.Key, Count = g.Count() });

            // 16. Статистика телефонів за роками
            var statsByYear = phones
                .GroupBy(p => p.ReleaseDate.Year)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
