namespace LAB1
{
    public class PersonConsoleUI
    {
        private readonly IPersonRepository _repository;

        public PersonConsoleUI(IPersonRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DisplayList(); break;
                    case "2": AddPerson(); break;
                    case "3": RemovePerson(); break;
                    case "4": EditPerson(); break;
                    case "5": QueryByAge(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n===== Меню керування списком людей =====");
            Console.WriteLine("1. Показати список");
            Console.WriteLine("2. Додати нового");
            Console.WriteLine("3. Видалити за ім'ям");
            Console.WriteLine("4. Редагувати за ім'ям");
            Console.WriteLine("5. Виконати запит (люди старші за вказаний вік)");
            Console.WriteLine("0. Повернутися до головного меню");
            Console.Write("Ваш вибір: ");
        }

        private void DisplayList()
        {
            Console.WriteLine("\n--- Поточний список ---");
            var people = _repository.GetAll();
            if (!people.Any())
            {
                Console.WriteLine("Список порожній.");
                return;
            }
            foreach (var p in people)
                Console.WriteLine(p);
        }

        private void AddPerson()
        {
            Console.Write("Ім'я: ");
            string name = Console.ReadLine();
            int age = ConsoleInputHandler.ReadInt("Вік: ", 0, 150);
            Console.Write("Місто: ");
            string city = Console.ReadLine();

            _repository.Add(new Person(name, age, city));
            Console.WriteLine("Додано.");
        }

        private void RemovePerson()
        {
            Console.Write("Введіть ім'я для видалення: ");
            string name = Console.ReadLine();
            int removed = _repository.RemoveByName(name);
            Console.WriteLine(removed > 0 ? $"Видалено {removed} запис(ів)." : "Особу не знайдено.");
        }

        private void EditPerson()
        {
            Console.Write("Введіть ім'я для редагування: ");
            string name = Console.ReadLine();
            var person = _repository.FindByName(name);

            if (person == null)
            {
                Console.WriteLine("Особу не знайдено.");
                return;
            }

            int? newAge = ConsoleInputHandler.ReadNullableInt($"Новий вік (поточний {person.Age}, Enter — залишити): ");
            if (newAge.HasValue)
                person.Age = newAge.Value;

            Console.Write($"Нове місто (поточне {person.City}, Enter — залишити): ");
            string cityInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cityInput))
                person.City = cityInput;

            Console.WriteLine("Оновлено.");
        }

        private void QueryByAge()
        {
            int minAge = ConsoleInputHandler.ReadInt("Мінімальний вік: ", 0, 150);
            var result = _repository.GetOlderThan(minAge);

            Console.WriteLine($"\n--- Люди старші за {minAge} років ---");
            if (!result.Any())
            {
                Console.WriteLine("Нікого не знайдено.");
                return;
            }
            foreach (var p in result)
                Console.WriteLine(p);
        }
    }
}