using System.Text;

namespace LB1_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            double tankCapacity = 0;
            double distanceAB = 0;
            double distanceBC = 0;
            double weight = 0;

            using (StreamReader streamReader = new StreamReader("info.txt"))
            {
                tankCapacity = double.Parse(streamReader.ReadLine());
                distanceAB = double.Parse(streamReader.ReadLine());
                distanceBC = double.Parse(streamReader.ReadLine());
                weight = double.Parse(streamReader.ReadLine());

                Console.WriteLine($"Ємність бака = {tankCapacity}");
                Console.WriteLine($"Відстань A-B = {distanceAB}");
                Console.WriteLine($"Відстань B-C = {distanceBC}");
                Console.WriteLine($"Вага вантажу = {weight}");
            }

            double fuelPerKm = weight switch
            {
                <= 500 => 1,
                <= 1000 => 4,
                <= 1500 => 7,
                <= 2000 => 9,
                _ => -1
            };

            if (fuelPerKm == -1)
            {
                Console.WriteLine("Літак не піднімає такий вантаж!");
                return;
            }

            double fuelNeededAB = distanceAB * fuelPerKm;
            double fuelNeededBC = distanceBC * fuelPerKm;

            if (fuelNeededAB > tankCapacity)
            {
                Console.WriteLine("Неможливо долетіти з A до B.");
                return;
            }

            if (fuelNeededBC > tankCapacity)
            {
                Console.WriteLine("Неможливо долетіти з B до C.");
                return;
            }

            double fuelLeftAtB = tankCapacity - fuelNeededAB;
            double refuelNeeded = Math.Max(0, fuelNeededBC - fuelLeftAtB);

            Console.WriteLine($"Мінімальна дозаправка в пункті B: {refuelNeeded} л");
        }
    }
}