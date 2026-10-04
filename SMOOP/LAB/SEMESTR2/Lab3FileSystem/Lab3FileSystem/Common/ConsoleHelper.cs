using System.Text;

namespace Lab3FileSystem.Common;

/// <summary>Допоміжні методи для введення даних з консолі.</summary>
public static class ConsoleHelper
{
    /// <summary>Запитує шлях до файлу (прибирає лапки, які додає drag-and-drop).</summary>
    public static string ReadPath(string prompt)
    {
        Console.Write(prompt);
        return (Console.ReadLine() ?? string.Empty).Trim().Trim('"');
    }

    /// <summary>Запитує шлях; якщо користувач натиснув лише Enter — повертає значення за замовчуванням.</summary>
    public static string ReadPathOrDefault(string prompt, string defaultPath)
    {
        Console.Write($"{prompt} [{defaultPath}]: ");
        string path = (Console.ReadLine() ?? string.Empty).Trim().Trim('"');
        return path.Length == 0 ? defaultPath : path;
    }

    /// <summary>Запитує непорожній рядок.</summary>
    public static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = (Console.ReadLine() ?? string.Empty).Trim();
            if (value.Length > 0) return value;
            Console.WriteLine("Поле не може бути порожнім.");
        }
    }

    /// <summary>Повертає null, якщо користувач натиснув лише Enter (поле не змінюється).</summary>
    public static string? ReadOptional(string prompt)
    {
        Console.Write(prompt);
        string value = (Console.ReadLine() ?? string.Empty).Trim();
        return value.Length == 0 ? null : value;
    }

    /// <summary>Запитує ціле число в заданому діапазоні.</summary>
    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Введіть ціле число від {min} до {max}.");
        }
    }

    /// <summary>Запитує підтвердження (т/н).</summary>
    public static bool Confirm(string prompt)
    {
        Console.Write($"{prompt} (т/н): ");
        return (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant() is "т" or "так" or "y" or "yes";
    }

    /// <summary>Розбиває рядок "а, б, в" на список непорожніх елементів.</summary>
    public static List<string> ParseList(string input) =>
        [.. input.Split([',', ';'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)];

    /// <summary>Багаторядкове введення: завершується рядком END.</summary>
    public static string ReadMultiline(string prompt)
    {
        Console.WriteLine(prompt);
        var sb = new StringBuilder();
        while (true)
        {
            string? line = Console.ReadLine();
            if (line is null || line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase)) break;
            sb.AppendLine(line);
        }
        return sb.ToString().TrimEnd();
    }
}
