namespace LB2_4
{
    internal class Program
    {
        static string GetGrade(double score)
        {
            if (score > 15) return "Відмінно! Ви справжній математик.";
            if (score >= 7) return "Добре. Гарне тренування!";
            if (score >= 3) return "Задовільно. Варто більше практикуватися.";
            return "Незадовільно. Відпочиньте, будемо чекати на вас знову";
        }
        static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Random random = new Random();

                int score = 0;
                int lives = 5; 
                int questionNumber = 1;

            Console.WriteLine("=== BRAIN TRAINING ===");
            Console.WriteLine("У вас є 5 спроб. Кожна помилка забирає одне життя.");
            Console.WriteLine("Щоб вийти з гри в будь-який момент, введіть: exit");
            Console.WriteLine("Починаємо!\n");

            do
                {
                    
                    int num1 = random.Next(1, 21);
                    int num2 = random.Next(1, 21);
                    int operationType = random.Next(1, 4);

                    int correctAnswer = 0;
                    string opSymbol = "";

                    switch (operationType)
                    {
                        case 1:
                            correctAnswer = num1 + num2;
                            opSymbol = "+";
                            break;
                        case 2:
                            correctAnswer = num1 - num2;
                            opSymbol = "-";
                            break;
                        case 3:
                            correctAnswer = num1 * num2;
                            opSymbol = "*";
                            break;
                    }

                    Console.WriteLine($"--- Приклад №{questionNumber} (Життів: {lives}) ---");
                    Console.Write($"{num1} {opSymbol} {num2} = ");

                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Ви вирішили завершити гру.");
                    break;
                }

                if (int.TryParse(input, out int userAnswer) && userAnswer == correctAnswer)
                {
                        Console.WriteLine("Правильно! +1 бал.");
                        score++;
                    }
                    else
                    {
                        lives--;
                        Console.WriteLine($"Неправильно! Правильна відповідь: {correctAnswer}");
                        if (lives > 0)
                            Console.WriteLine($"Залишилося життів: {lives}");
                    }

                    Console.WriteLine();
                    questionNumber++;

                } while (lives > 0);

                Console.WriteLine(new string('-', 60));
                Console.WriteLine("ГРА ЗАКІНЧИЛАСЯ!");
                Console.WriteLine($"Ваш підсумковий рахунок: {score}");

                Console.WriteLine($"Оцінка: {GetGrade(score)}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
                Console.ReadKey();
            }
        }
    }