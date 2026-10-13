using System;
using System.Linq;
using Lab3_FileSystem.Models;
using Lab3_FileSystem.Services;

namespace Lab3_FileSystem.Tasks
{
    public static class Task3_SongCollection
    {
        public static void Run()
        {
            SongManager manager = new SongManager();
            manager.LoadFromFile();

            while (true)
            {
                Console.WriteLine("\n--- ЗАВДАННЯ 3: КОЛЕКЦІЯ ПІСЕНЬ ---");
                Console.WriteLine("1. Додати пісню");
                Console.WriteLine("2. Видалити пісню");
                Console.WriteLine("3. Шукати за назвою");
                Console.WriteLine("4. Шукати за виконавцем");
                Console.WriteLine("5. Показати всі пісні");
                Console.WriteLine("6. Зберегти колекцію у файл");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Song newSong = new Song();
                        Console.Write("Назва пісні: "); newSong.Title = Console.ReadLine();
                        Console.Write("Автор тексту: "); newSong.AuthorFullName = Console.ReadLine();
                        Console.Write("Композитор: "); newSong.Composer = Console.ReadLine();
                        Console.Write("Рік написання: "); int.TryParse(Console.ReadLine(), out int year); newSong.Year = year;
                        Console.Write("Текст пісні: "); newSong.Lyrics = Console.ReadLine();
                        Console.Write("Виконавці (через кому): ");
                        newSong.Performers = Console.ReadLine().Split(',').Select(p => p.Trim()).ToList();

                        manager.AddSong(newSong);
                        break;
                    case "2":
                        Console.Write("Введіть назву пісні для видалення: ");
                        manager.RemoveSong(Console.ReadLine());
                        break;
                    case "3":
                        Console.Write("Введіть назву (або її частину): ");
                        manager.SearchByTitle(Console.ReadLine());
                        break;
                    case "4":
                        Console.Write("Введіть ім'я виконавця: ");
                        manager.SearchByPerformer(Console.ReadLine());
                        break;
                    case "5":
                        manager.PrintAll();
                        break;
                    case "6":
                        manager.SaveToFile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
        }
    }
}