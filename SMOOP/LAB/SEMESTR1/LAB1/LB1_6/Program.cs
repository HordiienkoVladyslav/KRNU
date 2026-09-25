using System.Text;

namespace LB1_6
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int number;
            Console.Write("Введіть число: ");
            number = Convert.ToInt32(Console.ReadLine());
            if (number < 1 || number > 100)
            {
                Console.WriteLine("Помилка: введено число поза діапазоном 1–100.");
                return;
            }

            {
                if (number % 3 == 0 && number % 5 == 0)
                    Console.WriteLine("Fizz Buzz");
                else if (number % 3 == 0)
                    Console.WriteLine("Fizz");
                else if (number % 5 == 0)
                    Console.WriteLine("Buzz");
                else
                    Console.WriteLine(number);
            }
        }
    }
}