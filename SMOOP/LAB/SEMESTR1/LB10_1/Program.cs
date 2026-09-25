using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        // Делегати Action (void, без параметрів)
        Action displayTime = () => Console.WriteLine($"Поточний час: {DateTime.Now.ToString("HH:mm:ss")}");
        Action displayDate = () => Console.WriteLine($"Поточна дата: {DateTime.Now.ToString("dd.MM.yyyy")}");
        Action displayDayOfWeek = () => Console.WriteLine($"Поточний день тижня: {DateTime.Now.DayOfWeek}");

        Console.WriteLine("--- Демонстрація Action ---");
        displayTime();
        displayDate();
        displayDayOfWeek();
        Console.WriteLine();



        // Делегати Predicate (повертають bool)


        Predicate<int> isPrime = n =>
        {
            if (n <= 1) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        };


        Predicate<int> isFibonacci = n =>
        {
            if (n < 0) return false;

            // Число є числом Фібоначчі тоді, коли (5*n^2 + 4) або (5*n^2 - 4) є ідеальним квадратом.
            Func<int, bool> isPerfectSquare = x =>
            {
                int s = (int)Math.Sqrt(x);
                return s * s == x;
            };

            return isPerfectSquare(5 * n * n + 4) || isPerfectSquare(5 * n * n - 4);
        };

        Console.WriteLine("--- Демонстрація Predicate ---");
        int testNum1 = 17;
        int testNum2 = 21;
        Console.WriteLine($"Число {testNum1} просте? {isPrime(testNum1)}");
        Console.WriteLine($"Число {testNum2} є числом Фібоначчі? {isFibonacci(testNum2)}");
        Console.WriteLine($"Число 8 є числом Фібоначчі? {isFibonacci(8)}");
        Console.WriteLine();



        // Делегати Func (повертають double)

        Func<double, double, double> triangleArea = (baseLen, height) => 0.5 * baseLen * height;

        Func<double, double, double> rectangleArea = (width, height) => width * height;

        Console.WriteLine("--- Демонстрація Func ---");
        double tBase = 5, tHeight = 8;
        double rWidth = 4, rHeight = 10;

        Console.WriteLine($"Площа трикутника (основа {tBase}, висота {tHeight}): {triangleArea(tBase, tHeight)}");
        Console.WriteLine($"Площа прямокутника (сторони {rWidth}x{rHeight}): {rectangleArea(rWidth, rHeight)}");
    }
}