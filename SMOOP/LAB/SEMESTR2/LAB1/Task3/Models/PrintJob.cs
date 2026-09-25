namespace LAB1
{
    public class PrintJob
    {
        public string User { get; set; }
        public int Priority { get; set; }
        public int Pages { get; set; }

        public PrintJob(string user, int priority, int pages)
        {
            User = user;
            Priority = priority;
            Pages = pages;
        }
    }
}