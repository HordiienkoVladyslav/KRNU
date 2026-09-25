namespace Module3_LB7
{
    class Program
    {
        static void Main(string[] args)
        {
            ZooService zooService = new ZooService();
            ConsoleInterface ui = new ConsoleInterface(zooService);

            ui.Run();
        }
    }
}