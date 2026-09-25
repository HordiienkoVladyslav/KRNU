namespace LAB1
{
    public interface IPrintHistoryExporter
    {
        void Export(IEnumerable<PrintRecord> history, string filePath);
    }
}