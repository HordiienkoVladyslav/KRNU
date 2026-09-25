using System;
using System.Text;

namespace SeasonAndWeekday
{
     public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть дату у форматі дд.мм.рррр: ");
            string input = Console.ReadLine();

            DateTime date = DateTime.ParseExact(input, "dd.MM.yyyy", null);

            int dayNumber = (int)date.DayOfWeek;

            string dayOfWeek = dayNumber switch
            {
                0 => "Sunday",
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                6 => "Saturday",
                _ => "Unknown"
            };

            string season = date.Month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => "Unknown"
            };

            Console.WriteLine($"{season} {dayOfWeek}");
        }
    }
}