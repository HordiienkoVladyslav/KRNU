namespace LAB1
{
    public class WordAnalysisConsoleUI
    {
        private readonly IFileProvider _fileProvider;
        private readonly IWordCounter _wordCounter;
        private readonly IStatsExporter _statsExporter;
        private readonly string _manifestFileName;

        public WordAnalysisConsoleUI(
            IFileProvider fileProvider,
            IWordCounter wordCounter,
            IStatsExporter statsExporter,
            string manifestFileName = "firstFile.txt")
        {
            _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
            _wordCounter = wordCounter ?? throw new ArgumentNullException(nameof(wordCounter));
            _statsExporter = statsExporter ?? throw new ArgumentNullException(nameof(statsExporter));
            _manifestFileName = manifestFileName;
        }

        public void Run()
        {
            _fileProvider.EnsureDemoFilesExist(_manifestFileName);

            List<string> fileNames = _fileProvider.ReadFileList(_manifestFileName);
            if (fileNames.Count == 0)
            {
                Console.WriteLine($"Список файлів у {_manifestFileName} порожній.");
                return;
            }

            bool continueAnalysis = true;
            while (continueAnalysis)
            {
                DisplayFileList(fileNames);
                int selectedIndex = RequestFileSelection(fileNames.Count);

                if (selectedIndex < 0) continue;

                string chosenFile = fileNames[selectedIndex];
                if (!_fileProvider.Exists(chosenFile))
                {
                    Console.WriteLine($"Файл {chosenFile} не знайдено на диску.");
                    continue;
                }

                ProcessFile(chosenFile);

                Console.Write("\nПроаналізувати ще один файл? (y/n): ");
                continueAnalysis = Console.ReadLine().Trim().Equals("y", StringComparison.OrdinalIgnoreCase);
            }
        }

        private void DisplayFileList(List<string> fileNames)
        {
            Console.WriteLine("\n--- Доступні файли для аналізу ---");
            for (int i = 0; i < fileNames.Count; i++)
                Console.WriteLine($"{i + 1}. {fileNames[i]}");
        }

        private int RequestFileSelection(int maxCount)
        {
            Console.Write("Оберіть номер файлу: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > maxCount)
            {
                Console.WriteLine("Невірний номер.");
                return -1;
            }
            return index - 1;
        }

        private void ProcessFile(string fileName)
        {
            string content = _fileProvider.ReadText(fileName);
            var stats = _wordCounter.CountWords(content);
            var sortedStats = stats.OrderByDescending(k => k.Value).ThenBy(k => k.Key).ToList();

            Console.WriteLine($"\n--- Статистика слів у файлі \"{fileName}\" ---");
            foreach (var kv in sortedStats)
                Console.WriteLine($"{kv.Key,-20} : {kv.Value}");

            Console.Write("\nЗберегти статистику у файл? (y/n): ");
            if (Console.ReadLine().Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                _statsExporter.Export(fileName, sortedStats);
            }
        }
    }
}