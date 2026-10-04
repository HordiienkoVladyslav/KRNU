using System.Text.Json;
using Lab3FileSystem.Common;
using Lab3FileSystem.Task3_Songs.Models;
using Lab3FileSystem.Task3_Songs.Services;

namespace Lab3FileSystem.Task3_Songs.UI;

/// <summary>Консольне меню для роботи з колекцією пісень.</summary>
public static class SongMenu
{
    private const string DefaultFile = "songs.json";

    public static void Run(SongCollectionManager manager)
    {
        while (true)
        {
            Console.WriteLine("\n--- Завдання 3. Колекція пісень ---");
            Console.WriteLine($"Пісень у колекції: {manager.Songs.Count}{(manager.IsDirty ? " (є незбережені зміни)" : "")}");
            Console.WriteLine("1. Додати пісню");
            Console.WriteLine("2. Видалити пісню за назвою");
            Console.WriteLine("3. Редагувати пісню");
            Console.WriteLine("4. Пошук пісні (назва / автор / виконавець)");
            Console.WriteLine("5. Показати всі пісні");
            Console.WriteLine("6. Показати пісні виконавця");
            Console.WriteLine("7. Зберегти колекцію у файл");
            Console.WriteLine("8. Завантажити колекцію з файлу");
            Console.WriteLine("0. Назад до головного меню");
            Console.Write("Ваш вибір: ");

            string? choice = Console.ReadLine()?.Trim();

            if (choice == "0")
            {
                if (manager.IsDirty && ConsoleHelper.Confirm("Є незбережені зміни. Зберегти перед виходом?"))
                    SafeExecute(() => Save(manager));
                return;
            }

            SafeExecute(() =>
            {
                switch (choice)
                {
                    case "1": AddSong(manager); break;
                    case "2": RemoveSong(manager); break;
                    case "3": EditSong(manager); break;
                    case "4": SearchSongs(manager); break;
                    case "5": PrintList(manager.Songs, "Колекція порожня."); break;
                    case "6": ShowByArtist(manager); break;
                    case "7": Save(manager); break;
                    case "8": Load(manager); break;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            });
        }
    }

    /// <summary>Виконує дію з обробкою типових помилок роботи з файлами.</summary>
    private static void SafeExecute(Action action)
    {
        try { action(); }
        catch (JsonException ex) { Console.WriteLine($"Помилка формату JSON: {ex.Message}"); }
        catch (IOException ex) { Console.WriteLine($"Помилка введення-виведення: {ex.Message}"); }
        catch (UnauthorizedAccessException ex) { Console.WriteLine($"Немає доступу до файлу: {ex.Message}"); }
        catch (Exception ex) { Console.WriteLine($"Непередбачена помилка: {ex.Message}"); }
    }

    private static void AddSong(SongCollectionManager manager)
    {
        var song = new Song
        {
            Title = ConsoleHelper.ReadNonEmpty("Назва пісні: "),
            Author = ConsoleHelper.ReadNonEmpty("П.І.Б. автора слів: "),
            Composer = ConsoleHelper.ReadNonEmpty("Композитор: "),
            Year = ConsoleHelper.ReadInt("Рік написання: ", 1000, DateTime.Now.Year),
            Lyrics = ConsoleHelper.ReadMultiline("Текст пісні (завершіть введення рядком END):"),
            Artists = ConsoleHelper.ParseList(ConsoleHelper.ReadNonEmpty("Виконавці (через кому): "))
        };

        Console.WriteLine(manager.Add(song) ? "Пісню додано." : "Пісня з такою назвою вже існує.");
    }

    private static void RemoveSong(SongCollectionManager manager)
    {
        string title = ConsoleHelper.ReadNonEmpty("Назва пісні для видалення: ");
        Console.WriteLine(manager.Remove(title) ? "Пісню видалено." : "Пісню не знайдено.");
    }

    private static void EditSong(SongCollectionManager manager)
    {
        string title = ConsoleHelper.ReadNonEmpty("Назва пісні для редагування: ");
        Song? song = manager.FindByTitle(title);
        if (song is null)
        {
            Console.WriteLine("Пісню не знайдено.");
            return;
        }

        Console.WriteLine("Поточні дані:");
        PrintSong(song);
        Console.WriteLine("\nНатисніть Enter, щоб залишити поле без змін.");

        string? newTitle = ConsoleHelper.ReadOptional($"Назва [{song.Title}]: ");
        string? author = ConsoleHelper.ReadOptional($"Автор слів [{song.Author}]: ");
        string? composer = ConsoleHelper.ReadOptional($"Композитор [{song.Composer}]: ");

        int? year = null;
        while (true)
        {
            string? input = ConsoleHelper.ReadOptional($"Рік [{song.Year}]: ");
            if (input is null) break;

            if (int.TryParse(input, out int parsed) && parsed >= 1000 && parsed <= DateTime.Now.Year)
            {
                year = parsed;
                break;
            }
            Console.WriteLine("Некоректний рік.");
        }

        string? lyrics = ConsoleHelper.Confirm("Змінити текст пісні?")
            ? ConsoleHelper.ReadMultiline("Новий текст (завершіть введення рядком END):")
            : null;

        string? artistsInput = ConsoleHelper.ReadOptional($"Виконавці [{string.Join(", ", song.Artists)}]: ");
        List<string>? artists = artistsInput is null ? null : ConsoleHelper.ParseList(artistsInput);

        var update = new SongUpdate(newTitle, author, composer, year, lyrics, artists);

        Console.WriteLine(manager.Edit(title, update) switch
        {
            EditResult.Success => "Пісню оновлено.",
            EditResult.DuplicateTitle => "Не вдалося оновити: пісня з такою назвою вже існує.",
            _ => "Пісню не знайдено."
        });
    }

    private static void SearchSongs(SongCollectionManager manager)
    {
        Console.WriteLine("Шукати за: 1 — назвою, 2 — автором, 3 — виконавцем");
        int choice = ConsoleHelper.ReadInt("Вибір: ", 1, 3);
        string query = ConsoleHelper.ReadNonEmpty("Пошуковий запит: ");

        SearchField field = choice switch
        {
            1 => SearchField.Title,
            2 => SearchField.Author,
            _ => SearchField.Artist
        };

        PrintList(manager.Search(field, query), "Нічого не знайдено.");
    }

    private static void ShowByArtist(SongCollectionManager manager)
    {
        string artist = ConsoleHelper.ReadNonEmpty("Ім'я виконавця: ");
        PrintList(manager.GetByArtist(artist), $"Пісень виконавця «{artist}» не знайдено.");
    }

    private static void Save(SongCollectionManager manager)
    {
        string path = ConsoleHelper.ReadPathOrDefault("Файл для збереження", DefaultFile);
        manager.SaveToFile(path);
        Console.WriteLine($"Колекцію збережено: {Path.GetFullPath(path)}");
    }

    private static void Load(SongCollectionManager manager)
    {
        string path = ConsoleHelper.ReadPathOrDefault("Файл для завантаження", DefaultFile);

        if (!File.Exists(path))
        {
            Console.WriteLine("Помилка: файл не знайдено.");
            return;
        }

        manager.LoadFromFile(path);
        Console.WriteLine($"Завантажено пісень: {manager.Songs.Count}");
    }

    private static void PrintList(IReadOnlyList<Song> songs, string emptyMessage)
    {
        if (songs.Count == 0)
        {
            Console.WriteLine(emptyMessage);
            return;
        }

        Console.WriteLine($"\nЗнайдено пісень: {songs.Count}");
        for (int i = 0; i < songs.Count; i++)
        {
            Console.WriteLine($"\n[{i + 1}]");
            PrintSong(songs[i]);
        }
    }

    private static void PrintSong(Song song)
    {
        Console.WriteLine($"  Назва:       {song.Title}");
        Console.WriteLine($"  Автор слів:  {song.Author}");
        Console.WriteLine($"  Композитор:  {song.Composer}");
        Console.WriteLine($"  Рік:         {song.Year}");
        Console.WriteLine($"  Виконавці:   {string.Join(", ", song.Artists)}");
        Console.WriteLine("  Текст:");
        foreach (string line in song.Lyrics.Split('\n'))
            Console.WriteLine($"    {line.TrimEnd('\r')}");
    }
}
