using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("  ГОЛОВНЕ МЕНЮ: ЛАБОРАТОРНА РОБОТА №4");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Завдання 1: Клас «Маршрут» (Серіалізація та пошук)");
            Console.WriteLine("2. Додаткове завдання: Додаток «Словники»");
            Console.WriteLine("3. Вихід");
            Console.Write("Оберіть пункт меню (1-3): ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await RouteManager.RunTaskAsync();
                    break;
                case "2":
                    DictionaryAppUI.Run();
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Натисніть Enter для продовження...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}