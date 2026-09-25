using System.Text.RegularExpressions;

namespace LAB1
{
    public class RegexWordCounter : IWordCounter
    {
        private readonly string _pattern;

        public RegexWordCounter(string pattern = @"[a-zа-яіїєґ']+")
        {
            _pattern = pattern;
        }

        public Dictionary<string, int> CountWords(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new Dictionary<string, int>();

            var words = Regex.Matches(text.ToLower(), _pattern)
                             .Select(m => m.Value)
                             .Where(w => w.Length > 0);

            var stats = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (stats.ContainsKey(word))
                    stats[word]++;
                else
                    stats[word] = 1;
            }

            return stats;
        }
    }
}