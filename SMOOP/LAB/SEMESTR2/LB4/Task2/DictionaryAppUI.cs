using System;
using System.IO;
using System.Linq;

public static class DictionaryAppUI
{
    private static DictionaryService service = new();

    public static void Run()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТКОВЕ ЗАВДАННЯ: СЛОВНИКИ ===");
            Console.WriteLine("1. Створити словник");
            Console.WriteLine("2. Керувати словником (додати/змінити/видалити слова)");
            Console.WriteLine("3. Шукати переклад слова");
            Console.WriteLine("4. Експортувати слово у файл");
            Console.WriteLine("5. Повернутися до головного меню");
            Console.Write("Оберіть пункт меню (1-5): ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateDictionaryUI();
                    break;
                case "2":
                    ManageDictionaryMenu();
                    break;
                case "3":
                    SearchTranslationUI();
                    break;
                case "4":
                    ExportWordUI();
                    break;
                case "5":
                    service.SaveData();
                    back = true;
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Натисніть Enter для продовження...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void CreateDictionaryUI()
    {
        Console.Clear();
        Console.WriteLine("--- Створення нового словника ---");
        Console.Write("Введіть назву словника (наприклад, 'Англо-український'): ");
        string name = Console.ReadLine()?.Trim() ?? string.Empty;
        service.CreateDictionary(name);
        Pause();
    }

    private static void ManageDictionaryMenu()
    {
        if (service.Dictionaries.Count == 0)
        {
            Console.WriteLine("Спершу створіть хоча б один словник!");
            Pause();
            return;
        }

        Console.Clear();
        Console.WriteLine("Наявні словники:");
        var keys = service.Dictionaries.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {keys[i]}");
        }
        Console.Write("Оберіть номер словника (або 0 для повернення): ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= keys.Count)
        {
            string dictName = keys[index - 1];
            DictionarySubMenu(service.Dictionaries[dictName]);
        }
    }

    private static void DictionarySubMenu(TranslationDictionary dict)
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine($"--- Словник: {dict.DictionaryName} ---");
            Console.WriteLine("1. Додати слово та переклад");
            Console.WriteLine("2. Замінити слово або переклад");
            Console.WriteLine("3. Видалити слово або переклад");
            Console.WriteLine("4. Переглянути всі слова");
            Console.WriteLine("5. Повернутися до попереднього меню");
            Console.Write("Ваш вибір: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Введіть слово: ");
                    string w = Console.ReadLine()?.Trim() ?? string.Empty;
                    Console.Write("Введіть переклад: ");
                    string t = Console.ReadLine()?.Trim() ?? string.Empty;
                    service.AddWord(dict, w, t);
                    Pause();
                    break;
                case "2":
                    UpdateWordOrTranslationUI(dict);
                    break;
                case "3":
                    DeleteWordOrTranslationUI(dict);
                    break;
                case "4":
                    ShowAllWordsUI(dict);
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    Pause();
                    break;
            }
        }
    }

    private static void UpdateWordOrTranslationUI(TranslationDictionary dict)
    {
        Console.Clear();
        Console.Write("Введіть слово, яке хочете змінити: ");
        string word = Console.ReadLine()?.Trim() ?? string.Empty;
        var item = dict.Items.FirstOrDefault(i => i.Word.Equals(word, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            Console.WriteLine("Слово не знайдено.");
            Pause();
            return;
        }

        Console.WriteLine($"1. Змінити саме слово '{item.Word}'");
        Console.WriteLine("2. Змінити конкретний переклад");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Введіть нове написання слова: ");
            string newWord = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(newWord))
            {
                item.Word = newWord;
                service.SaveData();
                Console.WriteLine("Слово оновлено!");
            }
        }
        else if (choice == "2")
        {
            Console.WriteLine("Наявні переклади:");
            for (int i = 0; i < item.Translations.Count; i++)
                Console.WriteLine($"{i + 1}. {item.Translations[i]}");

            Console.Write("Оберіть номер перекладу для зміни: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= item.Translations.Count)
            {
                Console.Write("Введіть новий текст перекладу: ");
                string newTr = Console.ReadLine()?.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(newTr))
                {
                    item.Translations[idx - 1] = newTr;
                    service.SaveData();
                    Console.WriteLine("Переклад оновлено!");
                }
            }
        }
        Pause();
    }

    private static void DeleteWordOrTranslationUI(TranslationDictionary dict)
    {
        Console.Clear();
        Console.Write("Введіть слово для видалення: ");
        string word = Console.ReadLine()?.Trim() ?? string.Empty;
        var item = dict.Items.FirstOrDefault(i => i.Word.Equals(word, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            Console.WriteLine("Слово не знайдено.");
            Pause();
            return;
        }

        Console.WriteLine("1. Видалити все слово (разом з усіма перекладами)");
        Console.WriteLine("2. Видалити окремий переклад");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            dict.Items.Remove(item);
            service.SaveData();
            Console.WriteLine("Слово та всі його переклади видалено.");
        }
        else if (choice == "2")
        {
            if (item.Translations.Count <= 1)
            {
                Console.WriteLine("Неможливо видалити переклад: це останній доступний варіант для цього слова!");
            }
            else
            {
                Console.WriteLine("Наявні переклади:");
                for (int i = 0; i < item.Translations.Count; i++)
                    Console.WriteLine($"{i + 1}. {item.Translations[i]}");

                Console.Write("Оберіть номер перекладу для видалення: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= item.Translations.Count)
                {
                    item.Translations.RemoveAt(idx - 1);
                    service.SaveData();
                    Console.WriteLine("Переклад видалено.");
                }
            }
        }
        Pause();
    }

    private static void ShowAllWordsUI(TranslationDictionary dict)
    {
        Console.Clear();
        Console.WriteLine($"--- Словник: {dict.DictionaryName} ---");
        if (dict.Items.Count == 0)
        {
            Console.WriteLine("Словник порожній.");
        }
        else
        {
            foreach (var item in dict.Items)
            {
                Console.WriteLine($"- {item.Word} : {string.Join(", ", item.Translations)}");
            }
        }
        Pause();
    }

    private static void SearchTranslationUI()
    {
        Console.Clear();
        Console.Write("Введіть слово для пошуку: ");
        string query = Console.ReadLine()?.Trim() ?? string.Empty;

        bool found = false;
        foreach (var dict in service.Dictionaries.Values)
        {
            var item = dict.Items.FirstOrDefault(i => i.Word.Equals(query, StringComparison.OrdinalIgnoreCase) ||
                                                       i.Translations.Any(t => t.Equals(query, StringComparison.OrdinalIgnoreCase)));
            if (item != null)
            {
                Console.WriteLine($"[Словник: {dict.DictionaryName}] {item.Word} -> {string.Join(", ", item.Translations)}");
                found = true;
            }
        }

        if (!found) Console.WriteLine("Нічого не знайдено.");
        Pause();
    }

    private static void ExportWordUI()
    {
        Console.Clear();
        Console.Write("Введіть слово для експорту: ");
        string query = Console.ReadLine()?.Trim() ?? string.Empty;

        foreach (var dict in service.Dictionaries.Values)
        {
            var item = dict.Items.FirstOrDefault(i => i.Word.Equals(query, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                string fileName = $"{item.Word}_export.txt";
                File.WriteAllText(fileName, $"Слово: {item.Word}\nПереклади: {string.Join(", ", item.Translations)}\nСловник: {dict.DictionaryName}");
                Console.WriteLine($"Експорт успішно виконано у файл: {fileName}");
                Pause();
                return;
            }
        }
        Console.WriteLine("Слово не знайдено для експорту.");
        Pause();
    }

    private static void Pause()
    {
        Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
        Console.ReadLine();
    }
}