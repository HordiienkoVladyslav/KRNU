using System;
using System.Collections.Generic;
using System.Text;

namespace LB7_Zoo
{
    public class ZooManager
    {
        static Animal[] waitList = Array.Empty<Animal>();
        static Enclosure[] enclosures = Array.Empty<Enclosure>();

        public static void ExecuteAddAnimalAction()
        {
            Console.WriteLine("\n--- РЕЄСТРАЦІЯ НОВОЇ ТВАРИНИ ---");
            Console.WriteLine("Оберіть вид: 1. Тигр | 2. Крокодил | 3. Кенгуру");
            string type = Console.ReadLine();


            Console.Write("Введіть ім'я: ");
            string name = Console.ReadLine();

            Console.Write("Денна норма корму (кг): ");
            double food = double.Parse(Console.ReadLine());

            Console.Write("Чи може розмножуватись у неволі? (д/н): ");
            bool canBreed = Console.ReadLine().ToLower() == "д";

            Console.Write("Чи є тварина соціальною (може жити з іншими)? (д/н): ");
            bool isSocial = Console.ReadLine().ToLower() == "д";

            // Створення екземпляра з повним набором даних
            Animal newAnimal = CreateAnimalWithFullParams(type, name, food, canBreed, isSocial);

            if (newAnimal != null)
            {
                ArrayHelper.AddElement(ref waitList, newAnimal);
                Console.WriteLine($"\nУспішно! {newAnimal.Name} доданий(а) до WaitList.");
            }
        }

        static Animal CreateAnimalWithFullParams(string type, string name, double food, bool breed, bool social)
        {
            return type switch
            {
                "1" => new Tiger(name, food, breed, social),
                "2" => new Crocodile(name, food, breed, social),
                "3" => new Kangaroo(name, food, breed, social),
                _ => null
            };
        }

        public static void ExecuteCreateEnclosureAction()
        {
            Console.Write("Назва вольєру: ");
            string title = Console.ReadLine();
            Console.Write("Місткість: ");
            if (int.TryParse(Console.ReadLine(), out int cap))
            {
                ArrayHelper.AddElement(ref enclosures, new Enclosure(title, cap));
                Console.WriteLine("Вольєр створено.");
            }
        }

        public static void ExecuteHousingAction()
        {
            if (waitList.Length == 0)
            {
                Console.WriteLine("Черга очікування порожня.");
                return;
            }

            // Беремо першу тварину з черги
            Animal animalToMove = waitList[0];
            Console.WriteLine($"\nСпроба розселити: {animalToMove.Name} ({animalToMove.Species})");

            Enclosure target = null;

            // Шукаємо підходящий вольєр через метод CanAccept
            foreach (var enc in enclosures)
            {
                if (enc.CanAccept(animalToMove))
                {
                    target = enc;
                    break;
                }
            }

            if (target != null)
            {
                target.AddAnimal(animalToMove);
                ArrayHelper.RemoveFirst(ref waitList);
                Console.WriteLine($"УСПІХ: {animalToMove.Name} оселено у вольєр '{target.Type}'.");
            }
            else
            {
                Console.WriteLine("ВІДМОВА: Не знайдено підходящого або вільного вольєру для цього виду.");
                Console.WriteLine("Порада: Створіть новий вольєр.");
            }
        }

        public static Enclosure FindAvailableEnclosure()
        {
            foreach (var enc in enclosures)
            {
                if (enc.CurrentCount < enc.AnimalsIn.Length) return enc;
            }
            return null;
        }

        public static void ShowZooStatus()
        {
            Console.WriteLine("\n--- ЗВІТ ПО ВОЛЬЄРАХ ---");
            foreach (var enc in enclosures)
            {
                Console.WriteLine($"Вольєр [{enc.Type}]: {enc.CurrentCount}/{enc.AnimalsIn.Length}");
            }
            Console.WriteLine($"У черзі (WaitList): {waitList.Length}");
        }

        public static void ShowFoodReport()
        {
            double monthlyTotal = (CalculateTotalDailyFood()) * 30;
            Console.WriteLine($"\nЗООПАРКУ ПОТРІБНО: {monthlyTotal:F2} кг корму на місяць.");
        }

        static double CalculateTotalDailyFood()
        {
            double sum = 0;
            foreach (var a in waitList) sum += a.DailyFoodAmount;
            foreach (var enc in enclosures)
            {
                for (int i = 0; i < enc.CurrentCount; i++)
                    sum += enc.AnimalsIn[i].DailyFoodAmount;
            }
            return sum;
        }

        public static void PauseAndClear()
        {
            Console.WriteLine("\nНатисніть клавішу для продовження...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
