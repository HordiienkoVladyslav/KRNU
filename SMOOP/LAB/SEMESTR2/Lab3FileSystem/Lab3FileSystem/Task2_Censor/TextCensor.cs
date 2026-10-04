using System.Text.RegularExpressions;

namespace Lab3FileSystem.Task2_Censor;

/// <summary>Результат цензурування.</summary>
/// <param name="Text">Зацензурований текст</param>
/// <param name="Replacements">Кількість замінених слів</param>
public sealed record CensorResult(string Text, int Replacements);

/// <summary>Логіка цензури (не залежить від консолі та файлів).</summary>
public static class TextCensor
{
    /// <summary>Розбирає вміст файлу зі словами: роздільники — кома, крапка з комою, перенос рядка.</summary>
    public static List<string> ParseBadWords(string raw) =>
        [.. raw.Split([',', ';', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
               .Distinct(StringComparer.OrdinalIgnoreCase)];

    /// <summary>
    /// Замінює кожне небажане слово (без урахування регістру) на зірочки тієї ж довжини.
    /// Шукаються цілі слова: "test" не зачепить "testing".
    /// </summary>
    public static CensorResult Censor(string text, IReadOnlyCollection<string> badWords)
    {
        if (badWords.Count == 0) return new CensorResult(text, 0);

        // Довші слова ставимо першими, щоб вони мали пріоритет у регулярному виразі
        string alternation = string.Join("|",
            badWords.OrderByDescending(w => w.Length).Select(Regex.Escape));

        // Межі слова: перед і після не повинно бути літери, цифри чи підкреслення
        string pattern = $@"(?<![\p{{L}}\p{{N}}_])(?:{alternation})(?![\p{{L}}\p{{N}}_])";

        int count = 0;
        string result = Regex.Replace(
            text,
            pattern,
            match =>
            {
                count++;
                return new string('*', match.Length);
            },
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        return new CensorResult(result, count);
    }
}
