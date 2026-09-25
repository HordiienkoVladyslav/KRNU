using LB7_Zoo;
using System;

namespace LB7_Zoo
{
    class Program
    {

        

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            RunMainMenu();
        }


        static void RunMainMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                RenderMenu();
                string choice = Console.ReadLine();
                isRunning = ProcessMenuChoice(choice);

                if (isRunning) ZooManager.PauseAndClear();
            }
        }


        static void RenderMenu()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("       СИСТЕМА УПРАВЛІННЯ ЗООПАРКОМ       ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Додати тварину у WaitList");
            Console.WriteLine("2. Розселити тварину у вольєр");
            Console.WriteLine("3. Створити додатковий вольєр");
            Console.WriteLine("4. МОНІТОРИНГ: Стан усіх вольєрів");
            Console.WriteLine("5. МОНІТОРИНГ: Кількість корму (місяць)");
            Console.WriteLine("0. Вихід");
            Console.Write("\nВаш вибір: ");
        }


        static bool ProcessMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1": ZooManager.ExecuteAddAnimalAction(); return true;
                case "2": ZooManager.ExecuteHousingAction(); return true;
                case "3": ZooManager.ExecuteCreateEnclosureAction(); return true;
                case "4": ZooManager.ShowZooStatus(); return true;
                case "5": ZooManager.ShowFoodReport(); return true;
                case "0": return false;
                default:
                    Console.WriteLine("Помилка: Невірна команда.");
                    return true;
            }
        }

        
    }
}