namespace LB2_3
{
        public class Program
    {
        static string GetGrade(double percent)
        {
            if (percent >= 90) return "5 (Відмінно)";
            if (percent >= 70) return "4 (Добре)";
            if (percent >= 50) return "3 (Задовільно)";
            return "2 (Незадовільно)";
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Перевірка знань таблиці множення ===");
            Console.WriteLine("Оберіть рівень складності:");
            Console.WriteLine("1 - Легкий");
            Console.WriteLine("2 - Середній");
            Console.WriteLine("3 - Складний");

            int level = Convert.ToInt32(Console.ReadLine());

            int min = 1;
            int max = 5;
            int questions = 5;

            switch (level)
            {
                case 1:
                    min = 1;
                    max = 5;
                    questions = 5;
                    break;

                case 2:
                    min = 1;
                    max = 10;
                    questions = 7;
                    break;

                case 3:
                    min = 1;
                    max = 12;
                    questions = 10;
                    break;

                default:
                    Console.WriteLine("Невірний вибір! Встановлено легкий рівень.");
                    break;
            }

            Random random = new Random();
            int correctAnswers = 0;

            for (int i = 1; i <= questions; i++)
            {
                int a = random.Next(min, max + 1);
                int b = random.Next(min, max + 1);

                Console.Write($"Питання {i}: {a} * {b} = ");
                int userAnswer = Convert.ToInt32(Console.ReadLine());

                if (userAnswer == a * b)
                {
                    Console.WriteLine("Правильно!");
                    correctAnswers++;
                }
                else
                {
                    Console.WriteLine($"Неправильно! Правильна відповідь: {a * b}");
                }

                Console.WriteLine();
            }

            double percent = (double)correctAnswers / questions * 100;

            Console.WriteLine("Результат");
            Console.WriteLine(new string( '-', 30));
            Console.WriteLine($"Правильних відповідей: {correctAnswers} з {questions}");
            Console.WriteLine($"Відсоток: {percent:F1}%");

            Console.WriteLine($"Оцінка: {GetGrade(percent)}");

            Console.ReadKey();
        }
    }
}