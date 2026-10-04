using Lab3FileSystem.Task1_TextStatistics;
using Lab3FileSystem.Task2_Censor;
using Lab3FileSystem.Task3_Songs.Services;
using Lab3FileSystem.Task3_Songs.UI;

namespace Lab3FileSystem.UI;

/// <summary>Головне меню: запуск трьох завдань лабораторної роботи.</summary>
public static class MainMenu
{
    public static void Run()
    {
        // Один менеджер на весь час роботи програми, щоб колекція не губилась при виході в меню
        var songManager = new SongCollectionManager();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=========== ЛАБОРАТОРНА РОБОТА №3 ===========");
            Console.WriteLine("     Взаємодія з файловою системою (System.IO)");
            Console.WriteLine("1. Завдання 1 — Статистика тексту з файлу");
            Console.WriteLine("2. Завдання 2 — Цензор");
            Console.WriteLine("3. Завдання 3 — Колекція пісень");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": TextStatisticsMenu.Run(); break;
                case "2": CensorMenu.Run(); break;
                case "3": SongMenu.Run(songManager); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір, спробуйте ще раз."); break;
            }
        }
    }
}
