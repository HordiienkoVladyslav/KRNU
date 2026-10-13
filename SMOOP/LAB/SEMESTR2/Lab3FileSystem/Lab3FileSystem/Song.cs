using System.Collections.Generic;

namespace Lab3_FileSystem.Models
{
    public class Song
    {
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public string Composer { get; set; }
        public int Year { get; set; }
        public string Lyrics { get; set; }
        public List<string> Performers { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"[{Year}] {Title}\nАвтор тексту: {AuthorFullName} | Композитор: {Composer}\nВиконавці: {string.Join(", ", Performers)}\nТекст:\n{Lyrics}\n";
        }
    }
}