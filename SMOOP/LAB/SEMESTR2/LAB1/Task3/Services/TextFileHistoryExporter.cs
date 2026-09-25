using System.Text;
namespace LAB1
{
    public class TextFileHistoryExporter : IPrintHistoryExporter
    {
        public void Export(IEnumerable<PrintRecord> history, string filePath)
        {
            var recordList = history.ToList();
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Статистика друку");
                foreach (var rec in recordList)
                {
                    writer.WriteLine(rec.ToString());
                }
                writer.WriteLine($"\nВсього завдань надруковано: {recordList.Count}");
                writer.WriteLine($"Всього сторінок: {recordList.Sum(r => r.Pages)}");
            }
        }
    }
}