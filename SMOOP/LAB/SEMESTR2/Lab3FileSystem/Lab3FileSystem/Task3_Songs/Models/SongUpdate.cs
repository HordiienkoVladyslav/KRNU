namespace Lab3FileSystem.Task3_Songs.Models;

/// <summary>Зміни для редагування пісні. Поля зі значенням null залишаються без змін.</summary>
public sealed record SongUpdate(
    string? Title = null,
    string? Author = null,
    string? Composer = null,
    int? Year = null,
    string? Lyrics = null,
    List<string>? Artists = null);
