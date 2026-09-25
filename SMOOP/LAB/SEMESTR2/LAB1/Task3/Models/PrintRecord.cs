namespace LAB1
{
    public class PrintRecord
    {
        public string User { get; set; }
        public DateTime Time { get; set; }
        public int Pages { get; set; }

        public override string ToString()
        {
            return $"{Time:dd.MM.yyyy HH:mm:ss} | {User,-15} | Сторінок: {Pages}";
        }
    }
}