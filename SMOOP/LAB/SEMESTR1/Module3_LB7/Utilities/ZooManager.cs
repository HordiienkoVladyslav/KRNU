namespace Module3_LB7
{
    public class ConsoleInterface
    {
        private readonly ZooService _zooService;

        public ConsoleInterface(ZooService zooService)
        {
            _zooService = zooService;
        }

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool isRunning = true;
            while (isRunning)
            {
                RenderMenu();
                string choice = Console.ReadLine();
                isRunning = ProcessChoice(choice);

                if (isRunning) PauseAndClear();
            }
        }

        private void RenderMenu()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("       СИСТЕМА УПРАВЛІННЯ ЗООПАРКОМ       ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Створити вольєр");
            Console.WriteLine("2. Зареєструвати та ОДРАЗУ заселити тварину");
            Console.WriteLine("3. МОНІТОРИНГ: Стан усіх вольєрів");
            Console.WriteLine("4. МОНІТОРИНГ: Розрахунок корму на місяць");
            Console.WriteLine("0. Вихід");
            Console.Write("\nВаш вибір: ");
        }

        private bool ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1": ExecuteCreateEnclosure(); return true;
                case "2": ExecuteAddAndHouseAnimal(); return true;
                case "3": ShowZooStatus(); return true;
                case "4": ShowFoodReport(); return true;
                case "0": return false;
                default:
                    Console.WriteLine("Помилка: Невірна команда.");
                    return true;
            }
        }

        private void ExecuteCreateEnclosure()
        {
            Console.Write("Тип/Назва вольєру: ");
            string type = Console.ReadLine();
            Console.Write("Місткість: ");
            if (int.TryParse(Console.ReadLine(), out int cap))
            {
                _zooService.AddEnclosure(type, cap);
                Console.WriteLine("Вольєр успішно створено.");
            }
        }

        private void ExecuteAddAndHouseAnimal()
        {
            var enclosures = _zooService.GetEnclosures();
            if (enclosures.Length == 0)
            {
                Console.WriteLine("Помилка: Спочатку створіть хоча б один вольєр!");
                return;
            }

            Console.WriteLine("\n--- РЕЄСТРАЦІЯ ТА ЗАСЕЛЕННЯ ТВАРИНИ ---");
            Console.WriteLine("Оберіть вид: 1. Тигр | 2. Крокодил | 3. Кенгуру");
            string typeId = Console.ReadLine();

            Console.Write("Введіть ім'я: ");
            string name = Console.ReadLine();

            Console.Write("Денна норма корму (кг): ");
            double food = double.Parse(Console.ReadLine());

            Console.Write("Чи може розмножуватись у неволі? (д/н): ");
            bool canBreed = Console.ReadLine().ToLower() == "д";

            Console.Write("Чи є тварина соціальною? (д/н): ");
            bool isSocial = Console.ReadLine().ToLower() == "д";

            IAnimal newAnimal = AnimalFactory.CreateAnimal(typeId, name, food, canBreed, isSocial);
            if (newAnimal == null)
            {
                Console.WriteLine("Помилка: Невірний тип тварини.");
                return;
            }

            // Показуємо список доступних вольєрів
            Console.WriteLine("\nДоступні вольєри:");
            for (int i = 0; i < enclosures.Length; i++)
            {
                Console.WriteLine($"{i}. Вольєр [{enclosures[i].Type}] (Зайнято: {enclosures[i].CurrentCount}/{enclosures[i].AnimalsIn.Length})");
            }

            Console.Write("Оберіть номер вольєру для заселення: ");
            if (int.TryParse(Console.ReadLine(), out int targetIndex))
            {
                if (_zooService.TryAccodomateAnimal(newAnimal, targetIndex, out string message))
                {
                    Console.WriteLine($"\n{message}");
                }
                else
                {
                    Console.WriteLine($"\n{message}");
                    Console.WriteLine("Тварина не була зареєстрована, оскільки умови проживання не підходять.");
                }
            }
        }

        private void ShowZooStatus()
        {
            Console.WriteLine("\n--- СТАН ВОЛЬЄРІВ ---");
            var enclosures = _zooService.GetEnclosures();
            if (enclosures.Length == 0) Console.WriteLine("Вольєри ще не створені.");

            foreach (var enc in enclosures)
            {
                Console.WriteLine($"Вольєр [{enc.Type}]: {enc.CurrentCount}/{enc.AnimalsIn.Length}");
                for (int i = 0; i < enc.CurrentCount; i++)
                {
                    Console.WriteLine($"   -> {enc.AnimalsIn[i]}");
                }
            }
        }

        private void ShowFoodReport()
        {
            double total = _zooService.CalculateMonthlyFoodTotal();
            Console.WriteLine($"\n[ЗВІТ]: Всього потрібно корму на місяць (30 днів): {total:F2} кг.");
        }

        private void PauseAndClear()
        {
            Console.WriteLine("\nНатисніть клавішу для продовження...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}