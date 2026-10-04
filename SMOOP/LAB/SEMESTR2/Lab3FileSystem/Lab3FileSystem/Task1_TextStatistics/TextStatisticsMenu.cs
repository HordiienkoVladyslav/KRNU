using Lab3FileSystem.Common;

namespace Lab3FileSystem.Task1_TextStatistics;

/// <summary>Консольний інтерфейс завдання 1.</summary>
public static class TextStatisticsMenu
{
    public static void Run()
    {
        Console.WriteLine("\n--- Завдання 1. Статистика тексту ---");
        string path = ConsoleHelper.ReadPath("Введіть шлях до текстового файлу: ");

        // Перевірка існування файлу
        if (!File.Exists(path))
        {
            Console.WriteLine("Помилка: файл не знайдено.");
            return;
        }

        try
        {
            string text = FileHelper.ReadAllText(path);
            TextStatistics stats = TextAnalyzer.Analyze(text);

            Console.WriteLine("\nРезультати аналізу:");
            Console.WriteLine($"Кількість речень:    {stats.Sentences}");
            Console.WriteLine($"Великих літер:       {stats.UpperCaseLetters}");
            Console.WriteLine($"Маленьких літер:     {stats.LowerCaseLetters}");
            Console.WriteLine($"Голосних літер:      {stats.Vowels}");
            Console.WriteLine($"Приголосних літер:   {stats.Consonants}");
            Console.WriteLine($"Цифр:                {stats.Digits}");
        }
        catch (IOException ex) { Console.WriteLine($"Помилка введення-виведення: {ex.Message}"); }
        catch (UnauthorizedAccessException ex) { Console.WriteLine($"Немає доступу до файлу: {ex.Message}"); }
        catch (Exception ex) { Console.WriteLine($"Непередбачена помилка: {ex.Message}"); }
    }
}
