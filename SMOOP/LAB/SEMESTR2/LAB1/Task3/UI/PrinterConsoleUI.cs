namespace LAB1
{
    public class PrinterConsoleUI
    {
        private readonly IPrinterService _printerService;
        private readonly IPrintHistoryExporter _exporter;

        public PrinterConsoleUI(IPrinterService printerService, IPrintHistoryExporter exporter)
        {
            _printerService = printerService ?? throw new ArgumentNullException(nameof(printerService));
            _exporter = exporter ?? throw new ArgumentNullException(nameof(exporter));
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                ShowMenu();
                switch (Console.ReadLine())
                {
                    case "1": AddJob(); break;
                    case "2": ProcessNext(); break;
                    case "3": ShowQueue(); break;
                    case "4": ShowStatistics(); break;
                    case "5": SaveStatistics(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n===== Черга друку принтера =====");
            Console.WriteLine("1. Додати завдання на друк");
            Console.WriteLine("2. Надрукувати наступне завдання (з черги)");
            Console.WriteLine("3. Показати поточну чергу");
            Console.WriteLine("4. Показати статистику друку");
            Console.WriteLine("5. Зберегти статистику у файл");
            Console.WriteLine("0. Повернутися до головного меню");
            Console.Write("Ваш вибір: ");
        }

        private void AddJob()
        {
            Console.Write("Ім'я користувача: ");
            string user = Console.ReadLine();
            int priority = ConsoleInputHandler.ReadInt("Пріоритет (1 - найвищий, 5 - найнижчий): ", 1, 5);
            int pages = ConsoleInputHandler.ReadInt("Кількість сторінок: ", 1, 10000);

            _printerService.EnqueueJob(new PrintJob(user, priority, pages));
            Console.WriteLine("Завдання додано в чергу.");
        }

        private void ProcessNext()
        {
            if (!_printerService.HasPendingJobs)
            {
                Console.WriteLine("Черга порожня.");
                return;
            }

            var job = _printerService.ProcessNextJob();
            Console.WriteLine($"Друкується: {job.User}, сторінок: {job.Pages}, пріоритет: {job.Priority}");
            Console.WriteLine("Завдання надруковано.");
        }

        private void ShowQueue()
        {
            var queueItems = _printerService.GetCurrentQueue().ToList();
            if (!queueItems.Any())
            {
                Console.WriteLine("Черга порожня.");
                return;
            }

            Console.WriteLine("\n--- Поточна черга (за пріоритетом) ---");
            foreach (var item in queueItems)
            {
                Console.WriteLine($"{item.Job.User,-15} | Пріоритет: {item.Priority} | Сторінок: {item.Job.Pages}");
            }
        }

        private void ShowStatistics()
        {
            var history = _printerService.GetHistory();
            if (!history.Any())
            {
                Console.WriteLine("Історія друку порожня.");
                return;
            }

            Console.WriteLine("\n--- Статистика друку ---");
            foreach (var rec in history)
                Console.WriteLine(rec);

            Console.WriteLine($"\nВсього завдань надруковано: {history.Count}");
            Console.WriteLine($"Всього сторінок: {history.Sum(r => r.Pages)}");
        }

        private void SaveStatistics()
        {
            var history = _printerService.GetHistory();
            if (!history.Any())
            {
                Console.WriteLine("Історія друку порожня, нема що зберігати.");
                return;
            }

            string fileName = "print_statistics.txt";
            _exporter.Export(history, fileName);
            Console.WriteLine($"Статистику збережено у файл {fileName}");
        }
    }
}