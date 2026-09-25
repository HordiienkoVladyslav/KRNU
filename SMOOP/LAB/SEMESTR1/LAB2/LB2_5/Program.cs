namespace LB2_5
{
    internal class Program
    {
        static Random random = new Random();

        static void PlayLevel(int min, int max, int rounds, int lives, int scoreMultiplier, ref int playerScore, ref int computerScore, ref bool lostRound)
        {
            for (int r = 1; r <= rounds; r++)
            {
                int secret = random.Next(min, max + 1);
                int currentLives = lives;

                Console.WriteLine($"\n=== Раунд {r} ===");
                Console.WriteLine($"Вгадайте число від {min} до {max}");
                Console.WriteLine($"Життів: {currentLives}");

                bool guessed = false;

                while (currentLives > 0)
                {
                    Console.Write("Ваше число: ");
                    int guess = Convert.ToInt32(Console.ReadLine());

                    if (guess == secret)
                    {
                        Console.WriteLine("Правильно!");
                        guessed = true;
                        break;
                    }

                    currentLives--;

                    Console.WriteLine("Неправильно.");

                    if (currentLives == 0)
                        break;

                    Console.Write("Хочете підказку? (y/n): ");
                    string hint = Console.ReadLine();

                    if (hint.ToLower() == "y")
                    {
                        currentLives--;

                        if (secret > guess)
                            Console.WriteLine("Підказка: загадане число БІЛЬШЕ");
                        else
                            Console.WriteLine("Підказка: загадане число МЕНШЕ");
                    }

                    Console.WriteLine($"Життів залишилось: {currentLives}");
                }

                if (guessed)
                {
                    int points = currentLives * scoreMultiplier;
                    playerScore += points;

                    Console.WriteLine($"Очки за раунд: {points}");
                }
                else
                {
                    int points = lives * scoreMultiplier;
                    computerScore += points;
                    lostRound = true;

                    Console.WriteLine($"Ви програли раунд. Число було: {secret}");
                    Console.WriteLine($"Очки комп'ютера: {points}");
                }

                Console.WriteLine($"Поточний рахунок -> Ви: {playerScore} | Комп'ютер: {computerScore}");
            }
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int playerScore = 0;
            int computerScore = 0;
            bool lostRound = false;

            Console.WriteLine("=== GUESS MY NUMBER ===");

            
            int level1Lives = 5;

            Console.WriteLine("\nРІВЕНЬ 1 (1-10)");
            PlayLevel(1, 10, 3, level1Lives, 5, ref playerScore, ref computerScore, ref lostRound);

            Console.WriteLine("\nПІДСУМОК ПІСЛЯ РІВНЯ 1");
            Console.WriteLine($"Ви: {playerScore} | Комп'ютер: {computerScore}");

            
            if (lostRound)
            {
                Console.WriteLine("\nВи програли хоча б один раунд. Другий рівень недоступний.");
                Console.WriteLine("ГРА ЗАКІНЧЕНА");
                return;
            }

            Console.Write("\nБажаєте перейти на рівень 2? (y/n): ");
            if (Console.ReadLine().ToLower() != "y")
            {
                Console.WriteLine("Гру завершено.");
                return;
            }

            

            int level2Lives = 22;

            Console.WriteLine("\nРІВЕНЬ 2 (10-100)");
            PlayLevel(10, 100, 2, level2Lives, 10, ref playerScore, ref computerScore, ref lostRound);

            Console.WriteLine("\n=== ФІНАЛЬНИЙ РЕЗУЛЬТАТ ===");
            Console.WriteLine($"Ви: {playerScore} | Комп'ютер: {computerScore}");

            if (playerScore > computerScore)
                Console.WriteLine("ВИ ПЕРЕМОГЛИ!");
            else
                Console.WriteLine("КОМП'ЮТЕР ПЕРЕМІГ!");
        }
    }
}