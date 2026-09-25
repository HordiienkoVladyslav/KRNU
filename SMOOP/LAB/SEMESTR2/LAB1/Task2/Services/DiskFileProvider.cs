namespace LAB1
{
    public class DiskFileProvider : IFileProvider
    {
        public bool Exists(string path) => File.Exists(path);

        public List<string> ReadFileList(string manifestPath)
        {
            if (!File.Exists(manifestPath))
                return new List<string>();

            return File.ReadAllLines(manifestPath)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .Select(l => l.Trim())
                       .ToList();
        }

        public string ReadText(string path) => File.ReadAllText(path);

        public void EnsureDemoFilesExist(string manifestPath)
        {
            if (!File.Exists(manifestPath))
            {
                Console.WriteLine($"Файл {manifestPath} не знайдено. Створюю демонстраційні файли...");
                File.WriteAllText("text1.txt", "Кіт спав на килимі. Кіт мурчав, кіт спав, а собака гавкав.");
                File.WriteAllText("text2.txt", "The quick brown fox jumps over the lazy dog. The dog barked.");
                File.WriteAllLines(manifestPath, new[] { "text1.txt", "text2.txt" });
            }
        }
    }
}