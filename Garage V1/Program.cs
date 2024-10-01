namespace Garage_V1;

class Program
{
    static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        ui.Run();
    }
}