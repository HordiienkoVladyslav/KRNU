using System;
using System.Globalization;

namespace LB9_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Birthday myBirthday = new Birthday(21, 12, 2007);

            IBirthdayService service = new BirthdayService();


            DayOfWeek birthDayOfWeek = service.GetBirthDayOfWeek(myBirthday);
            Console.WriteLine($"День тижня народження: {birthDayOfWeek}");

            int testYear = 2026;
            DayOfWeek dayIn2026 = service.GetDayOfWeekInYear(myBirthday, testYear);
            Console.WriteLine($"День тижня у {testYear} році: {dayIn2026}");

            int daysLeft = service.GetDaysUntilNextBirthday(myBirthday);
            Console.WriteLine($"Днів до наступного свята: {daysLeft}");

            Console.ReadLine();
        }
    }
}