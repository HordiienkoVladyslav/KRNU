using System;

namespace LAB1
{
    public static class ConsoleInputHandler
    {
        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max);

            return result;
        }

        public static int? ReadNullableInt(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return null;
        }
    }
}