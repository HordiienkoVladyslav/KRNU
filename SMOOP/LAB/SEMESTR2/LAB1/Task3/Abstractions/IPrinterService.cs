namespace LAB1
{
    public interface IPrinterService
    {
        void EnqueueJob(PrintJob job);
        PrintJob ProcessNextJob();
        IReadOnlyCollection<PrintRecord> GetHistory();
        IEnumerable<(PrintJob Job, int Priority)> GetCurrentQueue();
        bool HasPendingJobs { get; }
    }
}