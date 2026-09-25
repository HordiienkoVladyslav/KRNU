using System.Text;

namespace LB2_1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            double a;

            Console.WriteLine("Оберіть значення параметра a:");
            Console.WriteLine("1) a = 0");
            Console.WriteLine("2) a = 0.5");
            Console.WriteLine("3) a = 1");
            Console.WriteLine("4) a = 1.5");
            Console.WriteLine("5) a = 2");
            Console.Write("Ваш вибір: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    a = 0;
                    break;
                case 2:
                    a = 0.5;
                    break;
                case 3:
                    a = 1;
                    break;
                case 4:
                    a = 1.5;
                    break;
                case 5:
                    a = 2;
                    break;
                default:
                    Console.WriteLine("Неправильний вибір!");
                    return;
            }
            Console.WriteLine($"\nПараметр a = {a}");
                Console.WriteLine("   x\t  y(x)");
                Console.WriteLine(new string('-', 20));

            for (double x = 0; x <= Math.PI; x += Math.PI / 36)
                {
                    double y = Math.Exp(-x * a) * Math.Sin(x);
                    Console.WriteLine($"{x:F4}\t {y:F6}");
                }

            Console.ReadKey();
        }
    }
}