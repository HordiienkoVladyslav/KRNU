namespace LAB1
{
    public class PrinterService : IPrinterService
    {
        private readonly PriorityQueue<PrintJob, int> _queue = new PriorityQueue<PrintJob, int>();
        private readonly List<PrintRecord> _history = new List<PrintRecord>();

        public bool HasPendingJobs => _queue.Count > 0;

        public void EnqueueJob(PrintJob job)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));
            _queue.Enqueue(job, job.Priority);
        }

        public PrintJob ProcessNextJob()
        {
            if (_queue.Count == 0) return null;

            var job = _queue.Dequeue();
            _history.Add(new PrintRecord
            {
                User = job.User,
                Time = DateTime.Now,
                Pages = job.Pages
            });

            return job;
        }

        public IReadOnlyCollection<PrintRecord> GetHistory() => _history.AsReadOnly();

        public IEnumerable<(PrintJob Job, int Priority)> GetCurrentQueue()
        {
            return _queue.UnorderedItems
                         .Select(item => (item.Element, item.Priority))
                         .OrderBy(item => item.Priority);
        }
    }
}