namespace LB2;

class Program
{
    static void Main(string[] args)
    {
        int choise = int.TryParse(Console.ReadLine(), out int result) ? result : 0;


        switch (choise)
        {
            case 1:
                Manager_Task1.Run();
                break;
            case 2:
                Manager_Task2.Run();
                break;
            case 3:
                Manager_Task3.Run();
                break;
        }
    }
}
