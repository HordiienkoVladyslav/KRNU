using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab3_FileSystem.Tasks
{
    public static class Task2_Censor
    {
        public static void Run()
        {
            Console.WriteLine("\n--- ЗАВДАННЯ 2: ДОДАТОК «ЦЕНЗОР» ---");
            Console.Write("Введіть шлях до файлу з текстом: ");
            string textPath = Console.ReadLine();

            Console.Write("Введіть шлях до файлу зі словами для цензури (розділені пробілом/комою/новим рядком): ");
            string censorWordsPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(textPath) || string.IsNullOrWhiteSpace(censorWordsPath))
            {
                Console.WriteLine("Помилка: Шляхи до файлів не можуть бути порожніми.");
                return;
            }

            if (!File.Exists(textPath) || !File.Exists(censorWordsPath))
            {
                Console.WriteLine("Помилка: Один або обидва файли не знайдено.");
                return;
            }

            try
            {
                string text;
                using (StreamReader reader = new StreamReader(textPath))
                {
                    text = reader.ReadToEnd();
                }

                
                var censorWords = File.ReadAllText(censorWordsPath)
                                      .Split(new[] { ' ', '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Distinct();

                foreach (var word in censorWords)
                {
                    string stars = new string('*', word.Length);
                    string pattern = $@"\b{Regex.Escape(word)}\b";
                    text = Regex.Replace(text, pattern, stars, RegexOptions.IgnoreCase);
                }

                string resultPath = Path.Combine(Path.GetDirectoryName(textPath) ?? "", "censored_text.txt");

                using (StreamWriter writer = new StreamWriter(resultPath))
                {
                    writer.Write(text);
                }

                Console.WriteLine($"\nЦензурування завершено! Результат збережено у файл: {resultPath}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка введення-виведення при роботі з файлами: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася непередбачена помилка: {ex.Message}");
            }
        }
    }
}