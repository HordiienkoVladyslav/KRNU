using System;
using Lab3_FileSystem.Tasks;

namespace Lab3_FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== ЛАБОРАТОРНА РОБОТА №3 ===");
                Console.WriteLine("1. Завдання 1 (Статистична інформація про файл)");
                Console.WriteLine("2. Завдання 2 (Додаток «Цензор»)");
                Console.WriteLine("3. Завдання 3 (Колекція пісень)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1_Statistics.Run(); break;
                    case "2": Task2_Censor.Run(); break;
                    case "3": Task3_SongCollection.Run(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір. Спробуйте ще раз."); break;
                }
            }
        }
    }
}