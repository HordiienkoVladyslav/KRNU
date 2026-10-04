namespace Lab3FileSystem.Task3_Songs.Models;

/// <summary>Модель пісні.</summary>
public class Song
{
    /// <summary>Назва пісні.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>П.І.Б. автора слів.</summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>Композитор.</summary>
    public string Composer { get; set; } = string.Empty;

    /// <summary>Рік написання.</summary>
    public int Year { get; set; }

    /// <summary>Текст пісні.</summary>
    public string Lyrics { get; set; } = string.Empty;

    /// <summary>Список виконавців.</summary>
    public List<string> Artists { get; set; } = [];
}
