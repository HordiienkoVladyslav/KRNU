using System.Text;

namespace LAB1
{
    public class TextFileStatsExporter : IStatsExporter
    {
        public void Export(string sourceFileName, IEnumerable<KeyValuePair<string, int>> stats)
        {
            string outFile = $"stats_{Path.GetFileNameWithoutExtension(sourceFileName)}.txt";
            using (var writer = new StreamWriter(outFile, false, Encoding.UTF8))
            {
                writer.WriteLine($"Статистика слів у файлі \"{sourceFileName}\"");
                foreach (var kv in stats)
                {
                    writer.WriteLine($"{kv.Key,-20} : {kv.Value}");
                }
            }
            Console.WriteLine($"Збережено у {outFile}");
        }
    }
}