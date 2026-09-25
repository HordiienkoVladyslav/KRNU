using System;

namespace LB9_1
{
    public struct Birthday
    {
        public DateTime Date { get; private set; }

        public Birthday(int day, int month, int year)
        {
            try
            {
                Date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Помилка: Введено некоректну дату! Встановлено поточну дату за замовчуванням.");
                Date = DateTime.Today;
            }
        }
    }
}