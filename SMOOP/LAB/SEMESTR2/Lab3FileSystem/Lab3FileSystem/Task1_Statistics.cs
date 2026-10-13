using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab3_FileSystem.Tasks
{
    public static class Task1_Statistics
    {
        public static void Run()
        {
            Console.WriteLine("\n--- ЗАВДАННЯ 1: СТАТИСТИКА ФАЙЛУ ---");
            Console.Write("Введіть шлях до текстового файлу: ");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Помилка: Файл не знайдено.");
                return;
            }

            string text = File.ReadAllText(filePath);

            int sentenceCount = Regex.Matches(text, @"[^.!?]+[.!?]").Count;
            if (sentenceCount == 0 && text.Trim().Length > 0) sentenceCount = 1;

            int upperCaseCount = text
                                .Count(char
                                .IsUpper);
            int lowerCaseCount = text
                                .Count(char
                                .IsLower);
            int digitCount = text
                            .Count(char
                            .IsDigit);

            string golosni = "АаЕеЄєИиІіЇїОоУуЮюЯяAaEeIiOoUuYy";
            int golosniCount = text
                            .Count(c => char
                            .IsLetter(c) && golosni
                            .Contains(c));
            int prigolosniCount = text
                                .Count(c => char
                                .IsLetter(c) && !golosni
                                .Contains(c));

            Console.WriteLine("\n=== Статистика ===");
            Console.WriteLine($"Кількість речень: {sentenceCount}");
            Console.WriteLine($"Кількість великих літер: {upperCaseCount}");
            Console.WriteLine($"Кількість маленьких літер: {lowerCaseCount}");
            Console.WriteLine($"Кількість голосних літер: {golosniCount}");
            Console.WriteLine($"Кількість приголосних літер: {prigolosniCount}");
            Console.WriteLine($"Кількість цифр: {digitCount}");
        }
    }
}