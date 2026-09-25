using System;

    public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double[] xZnach = { 0.5, Math.Sqrt(2) / 2, -1.0 };
        const double EPS = 1e-6;

        foreach (double x in xZnach)
        {
            double k = 0;
            double sum = 0;
            double chlen = x;

            while (Math.Abs(chlen) >= EPS)
            {
                sum += chlen;
                chlen *= (x * x * (2 * k + 1) * (2 * k + 1)) / ((2 * k + 2) * (2 * k + 3));
                k++;
            }

            double znachY = Math.Asin(x);

            Console.WriteLine($"x = {x}");
            Console.WriteLine($"S(x) = {sum}");
            Console.WriteLine($"y(x) = {znachY}");
            Console.WriteLine($"Різниця = {Math.Abs(sum - znachY)}");
            Console.WriteLine(new string('-', 40));
        }
    }
}