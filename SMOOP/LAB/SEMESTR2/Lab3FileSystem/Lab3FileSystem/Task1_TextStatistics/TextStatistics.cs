namespace Lab3FileSystem.Task1_TextStatistics;

/// <summary>Результат аналізу тексту.</summary>
/// <param name="Sentences">Кількість речень</param>
/// <param name="UpperCaseLetters">Великі літери</param>
/// <param name="LowerCaseLetters">Маленькі літери</param>
/// <param name="Vowels">Голосні</param>
/// <param name="Consonants">Приголосні</param>
/// <param name="Digits">Цифри</param>
public sealed record TextStatistics(
    int Sentences,
    int UpperCaseLetters,
    int LowerCaseLetters,
    int Vowels,
    int Consonants,
    int Digits);
