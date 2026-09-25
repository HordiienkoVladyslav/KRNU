namespace LAB1
{
    public interface IStatsExporter
    {
        void Export(string sourceFileName, IEnumerable<KeyValuePair<string, int>> stats);
    }
}