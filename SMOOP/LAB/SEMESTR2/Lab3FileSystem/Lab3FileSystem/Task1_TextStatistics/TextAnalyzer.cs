namespace Lab3FileSystem.Task1_TextStatistics;

/// <summary>Підрахунок статистики тексту (не залежить від консолі та файлів).</summary>
public static class TextAnalyzer
{
    private const string UkrainianVowels = "аеєиіїоуюя";
    private const string EnglishVowels = "aeiouy";

    public static TextStatistics Analyze(string text)
    {
        int sentences = 0, upper = 0, lower = 0, vowels = 0, consonants = 0, digits = 0;
        bool previousWasTerminator = false;

        foreach (char c in text)
        {
            // Речення: послідовність знаків . ! ? (наприклад "..." чи "?!") вважаємо одним закінченням
            bool isTerminator = c is '.' or '!' or '?';
            if (isTerminator && !previousWasTerminator) sentences++;
            previousWasTerminator = isTerminator;

            if (char.IsAsciiDigit(c)) digits++;

            if (!char.IsLetter(c)) continue;

            // char.IsUpper / IsLower коректно обробляють А-Я, І, Ї, Є, Ґ та а-я, і, ї, є, ґ
            if (char.IsUpper(c)) upper++;
            else if (char.IsLower(c)) lower++;

            char lc = char.ToLowerInvariant(c);
            if (UkrainianVowels.Contains(lc) || EnglishVowels.Contains(lc))
                vowels++;
            else if (lc is not ('ь' or 'ъ')) // м'який/твердий знак не є приголосними
                consonants++;
        }

        return new TextStatistics(sentences, upper, lower, vowels, consonants, digits);
    }
}
