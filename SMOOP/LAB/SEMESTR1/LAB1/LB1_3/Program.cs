using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;


        int score = 0;
        int answer;

        Console.WriteLine("Жартівливий тест «Перевірю свої можливості»\n");

        Console.Write("1. Професор ліг спати о 8 годині, а встав о 9 годині. Кількість годин сну професору? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 1) score++;

        Console.Write("2. На двох руках десять пальців. Скільки пальців на 10 руках? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 50) score++;

        Console.Write("3. Скільки цифр у дюжині? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 2) score++;

        Console.Write("4. Скільки потрібно зробити розпилів, щоб розпиляти колоду на 12 частин? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 11) score++;

        Console.Write("5. Лікар зробив три уколи в інтервалі 30 хвилин. Скільки часу він витратив? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 30) score++;

        Console.Write("6. Скільки цифр 9 в інтервалі 1–100? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 1) score++;

        Console.Write("7. Пастух мав 30 овець. Усі, окрім однієї, розбіглися. Скільки овець лишилося? ");
        answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 1) score++;

        Console.WriteLine("\nКількість правильних відповідей: " + score);

        switch (score)
        {
            case 7:
                Console.WriteLine("Геній");
                break;
            case 6:
                Console.WriteLine("Ерудит");
                break;
            case 5:
                Console.WriteLine("Нормальний");
                break;
            case 4:
                Console.WriteLine("Здібності середні");
                break;
            case 3:
                Console.WriteLine("Здібності нижче середнього");
                break;
            default:
                Console.WriteLine("Вам треба відпочити!");
                break;
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}