using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== ЛЯМБДА-ВИРАЗИ ===\n");

        Func<int[], int> countMultiplesOfSeven = array =>
        {
            int count = 0;
            foreach (int number in array)
            {

                if (number % 7 == 0 && number != 0)
                {
                    count++;
                }
            }
            return count;
        };


        Func<int[], int> countPositiveNumbers = array =>
        {
            int count = 0;
            foreach (int number in array)
            {
                if (number > 0)
                {
                    count++;
                }
            }
            return count;
        };


        Func<DateTime, bool> isProgrammersDay = date =>
        {
            return date.DayOfYear == 256;
        };


        Func<string, string[], bool> containsAnyWord = (text, words) =>
        {
            foreach (string word in words)
            {

                if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    return true; 
                }
            }
            return false; 
        };



        // КОД ДЛЯ ТЕСТУВАННЯ РОБОТИ ПОБУДОВАНИХ ЛЯМБД



        int[] testNumbers = { 14, -5, 7, 0, 22, 21, -7, 35, 8 };
        Console.WriteLine($"Початковий масив чисел: [{string.Join(", ", testNumbers)}]");

        int sevens = countMultiplesOfSeven(testNumbers);
        Console.WriteLine($"1. Кількість чисел, кратних 7: {sevens} (Це: 14, 7, 21, 35)");

        int positives = countPositiveNumbers(testNumbers);
        Console.WriteLine($"2. Кількість позитивних чисел: {positives} (Це: 14, 7, 22, 21, 35, 8)\n");



        DateTime dateLeap = new DateTime(2024, 9, 12);
        DateTime dateNormal = new DateTime(2026, 9, 13);
        DateTime dateRandom = new DateTime(2026, 6, 4);

        Console.WriteLine($"3. Перевірка на День програміста (256-й день року):");
        Console.WriteLine($"   Чи є {dateLeap:dd.MM.yyyy} Днем програміста? -> {isProgrammersDay(dateLeap)}");
        Console.WriteLine($"   Чи є {dateNormal:dd.MM.yyyy} Днем програміста? -> {isProgrammersDay(dateNormal)}");
        Console.WriteLine($"   Чи є {dateRandom:dd.MM.yyyy} Днем програміста? -> {isProgrammersDay(dateRandom)}\n");



        string sampleText = "На цій лабораторній роботі ми практикуємо делегати, події та лямбда-вирази в C#.";
        string[] keywordsToFind = { "лямбда-вирази", "програмування", "C#" };
        string[] fakeKeywords = { "C++", "Python" };

        Console.WriteLine($"4. Аналіз тексту: \"{sampleText}\"");
        Console.WriteLine($"   Шукаємо слова [лямбда-вирази, програмування, C#]: {containsAnyWord(sampleText, keywordsToFind)}");
        Console.WriteLine($"   Шукаємо слова [Java, Python]: {containsAnyWord(sampleText, fakeKeywords)}");
    }
}