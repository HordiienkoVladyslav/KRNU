using System.Text.Encodings.Web;
using System.Text.Json;
using Lab3FileSystem.Task3_Songs.Models;

namespace Lab3FileSystem.Task3_Songs.Services;

/// <summary>Керування колекцією пісень: додавання, видалення, редагування, пошук, збереження/завантаження JSON.</summary>
public class SongCollectionManager
{
    private List<Song> _songs = [];

    // Налаштування JSON: відступи + кирилиця без екранування (\u0414...)
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>Усі пісні колекції (тільки для читання).</summary>
    public IReadOnlyList<Song> Songs => _songs;

    /// <summary>Чи є незбережені зміни.</summary>
    public bool IsDirty { get; private set; }

    /// <summary>Додає пісню. Повертає false, якщо пісня з такою назвою вже є.</summary>
    public bool Add(Song song)
    {
        if (FindByTitle(song.Title) is not null) return false;

        _songs.Add(song);
        IsDirty = true;
        return true;
    }

    /// <summary>Видаляє пісню за назвою. Повертає true, якщо щось видалено.</summary>
    public bool Remove(string title)
    {
        int removed = _songs.RemoveAll(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (removed > 0) IsDirty = true;
        return removed > 0;
    }

    /// <summary>Редагує пісню за назвою.</summary>
    public EditResult Edit(string title, SongUpdate update)
    {
        Song? song = FindByTitle(title);
        if (song is null) return EditResult.NotFound;

        // Нова назва не має дублювати іншу пісню
        if (update.Title is not null &&
            !update.Title.Equals(song.Title, StringComparison.OrdinalIgnoreCase) &&
            FindByTitle(update.Title) is not null)
            return EditResult.DuplicateTitle;

        if (update.Title is not null) song.Title = update.Title;
        if (update.Author is not null) song.Author = update.Author;
        if (update.Composer is not null) song.Composer = update.Composer;
        if (update.Year is not null) song.Year = update.Year.Value;
        if (update.Lyrics is not null) song.Lyrics = update.Lyrics;
        if (update.Artists is not null) song.Artists = update.Artists;

        IsDirty = true;
        return EditResult.Success;
    }

    /// <summary>Точний пошук пісні за назвою (без урахування регістру).</summary>
    public Song? FindByTitle(string title) =>
        _songs.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

    /// <summary>Пошук за назвою, автором або виконавцем (часткове співпадіння без урахування регістру).</summary>
    public List<Song> Search(SearchField field, string query) =>
        [.. _songs.Where(s => field switch
        {
            SearchField.Title => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase),
            SearchField.Author => s.Author.Contains(query, StringComparison.OrdinalIgnoreCase),
            SearchField.Artist => s.Artists.Any(a => a.Contains(query, StringComparison.OrdinalIgnoreCase)),
            _ => false
        })];

    /// <summary>Усі пісні вказаного виконавця (точний збіг імені без урахування регістру).</summary>
    public List<Song> GetByArtist(string artist) =>
        [.. _songs.Where(s => s.Artists.Any(a => a.Equals(artist, StringComparison.OrdinalIgnoreCase)))];

    /// <summary>Зберігає колекцію у JSON-файл.</summary>
    public void SaveToFile(string path)
    {
        using var stream = File.Create(path);
        JsonSerializer.Serialize(stream, _songs, JsonOptions);
        IsDirty = false;
    }

    /// <summary>Завантажує колекцію з JSON-файлу (замінює поточну).</summary>
    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Файл не знайдено.", path);

        using var stream = File.OpenRead(path);
        _songs = JsonSerializer.Deserialize<List<Song>>(stream, JsonOptions) ?? [];
        IsDirty = false;
    }
}
