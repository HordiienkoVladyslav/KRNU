using Lab3FileSystem.Common;

namespace Lab3FileSystem.Task2_Censor;

/// <summary>Консольний інтерфейс завдання 2.</summary>
public static class CensorMenu
{
    private const string OutputFileName = "censored_output.txt";

    public static void Run()
    {
        Console.WriteLine("\n--- Завдання 2. Цензор ---");
        string textPath = ConsoleHelper.ReadPath("Шлях до основного текстового файлу: ");
        string wordsPath = ConsoleHelper.ReadPath("Шлях до файлу з небажаними словами: ");

        if (!File.Exists(textPath)) { Console.WriteLine("Помилка: основний файл не знайдено."); return; }
        if (!File.Exists(wordsPath)) { Console.WriteLine("Помилка: файл зі словами не знайдено."); return; }

        try
        {
            string text = FileHelper.ReadAllText(textPath);
            List<string> badWords = TextCensor.ParseBadWords(FileHelper.ReadAllText(wordsPath));

            if (badWords.Count == 0)
            {
                Console.WriteLine("Файл зі словами порожній — цензурувати нічого.");
                return;
            }

            CensorResult result = TextCensor.Censor(text, badWords);

            // Зберігаємо результат поруч із вихідним файлом
            string directory = Path.GetDirectoryName(Path.GetFullPath(textPath)) ?? Directory.GetCurrentDirectory();
            string outputPath = Path.Combine(directory, OutputFileName);
            FileHelper.WriteAllText(outputPath, result.Text);

            Console.WriteLine($"\nНебажаних слів у списку: {badWords.Count}");
            Console.WriteLine($"Замінено входжень:       {result.Replacements}");
            Console.WriteLine("----- Зацензурований текст -----");
            Console.WriteLine(result.Text);
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Результат збережено у файл: {outputPath}");
        }
        catch (IOException ex) { Console.WriteLine($"Помилка введення-виведення: {ex.Message}"); }
        catch (UnauthorizedAccessException ex) { Console.WriteLine($"Немає доступу до файлу: {ex.Message}"); }
        catch (Exception ex) { Console.WriteLine($"Непередбачена помилка: {ex.Message}"); }
    }
}
