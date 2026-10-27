using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lab3_FileSystem.Models;

namespace Lab3_FileSystem.Services
{
    public class SongManager
    {
        private List<Song> songs = new List<Song>();
        private readonly string filePath = "songs_collection.txt";

        public void AddSong(Song song)
        {
            songs.Add(song);
            Console.WriteLine("Пісню успішно додано.");
        }

        public void RemoveSong(string title)
        {
            int removed = songs
                         .RemoveAll(s => s.Title
                         .Equals(title, StringComparison.OrdinalIgnoreCase));
            if (removed > 0) Console.WriteLine("Пісню видалено.");
            else Console.WriteLine("Пісню з такою назвою не знайдено.");
        }

        public void SearchByTitle(string title)
        {
            var result = songs
                        .Where(s => s.Title
                        .Contains(title, StringComparison.OrdinalIgnoreCase));
            PrintSongs(result);
        }

        public void SearchByPerformer(string performer)
        {
            var result = songs
                        .Where(s => s.Performers
                        .Any(p => p
                        .Contains(performer, StringComparison.OrdinalIgnoreCase)));
            PrintSongs(result);
        }

       
        public void SaveToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (var song in songs)
                    {
                        writer.WriteLine(song.Title);
                        writer.WriteLine(song.AuthorFullName);
                        writer.WriteLine(song.Composer);
                        writer.WriteLine(song.Year);
                        writer.WriteLine(string.Join(";", song.Performers));

                        string cleanLyrics = (song.Lyrics ?? "").Replace("\r\n", "\\n").Replace("\n", "\\n");
                        writer.WriteLine(cleanLyrics);

                        writer.WriteLine("==="); // Роздільник між піснями
                    }
                }
                Console.WriteLine($"Колекцію збережено у файл: {Path.GetFullPath(filePath)}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка введення-виведення при збереженні файлу: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася непередбачена помилка під час збереження: {ex.Message}");
            }
        }

 
        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл збереження не знайдено.");
                return;
            }

            try
            {
                songs.Clear();
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        string title = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(title)) continue;

                        string author = reader.ReadLine();
                        string composer = reader.ReadLine();
                        int.TryParse(reader.ReadLine(), out int year);
                        string performersStr = reader.ReadLine();
                        string lyricsStr = reader.ReadLine();
                        reader.ReadLine(); // Зчитуємо та пропускаємо роздільник "==="

                        var performers = (performersStr ?? "")
                            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => p.Trim())
                            .ToList();

                        string lyrics = (lyricsStr ?? "").Replace("\\n", Environment.NewLine);

                        songs.Add(new Song
                        {
                            Title = title,
                            AuthorFullName = author,
                            Composer = composer,
                            Year = year,
                            Performers = performers,
                            Lyrics = lyrics
                        });
                    }
                }
                Console.WriteLine("Колекцію успішно завантажено з текстового файлу.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка введення-виведення при читанні файлу: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася непередбачена помилка під час завантаження: {ex.Message}");
            }
        }

        public void PrintAll() => PrintSongs(songs);

        private void PrintSongs(IEnumerable<Song> collection)
        {
            if (!collection.Any())
            {
                Console.WriteLine("Нічого не знайдено.");
                return;
            }
            foreach (var song in collection)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(song);
            }
            Console.WriteLine(new string('-', 40));
        }
    }
}