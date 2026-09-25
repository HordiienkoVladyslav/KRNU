using System.Text;

namespace LAB1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n================ ЛАБОРАТОРНА РОБОТА №1 ================");
                Console.WriteLine("1. Завдання 1 (Управління списком Person)");
                Console.WriteLine("2. Завдання 2 (Аналіз частоти слів у файлах)");
                Console.WriteLine("3. Завдання 3 (Симуляція черги друку принтера)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть завдання: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        RunTask1();
                        break;
                    case "2":
                        RunTask2();
                        break;
                    case "3":
                        RunTask3();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void RunTask1()
        {
            var initialData = new List<Person>
            {
                new Person("Tom", 25, "Київ"),
                new Person("Bob", 32, "Львів"),
                new Person("Alice", 19, "Одеса"),
                new Person("Sam", 41, "Харків")
            };

            var repo = new InMemoryPersonRepository(initialData);
            var ui = new PersonConsoleUI(repo);
            ui.Run();
        }

        static void RunTask2()
        {
            var fileProvider = new DiskFileProvider();
            var counter = new RegexWordCounter();
            var exporter = new TextFileStatsExporter();
            var ui = new WordAnalysisConsoleUI(fileProvider, counter, exporter);
            ui.Run();
        }

        static void RunTask3()
        {
            var printerService = new PrinterService();
            var exporter = new TextFileHistoryExporter();
            var ui = new PrinterConsoleUI(printerService, exporter);
            ui.Run();
        }
    }
}