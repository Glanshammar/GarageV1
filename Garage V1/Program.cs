namespace Garage_V1;

class Program
{
    static void Main(string[] args)
    {
        if(!JsonHandler.ValidateFile("GarageV1.json"))
            JsonHandler.Create("GarageV1.json");
        
        ConsoleUI ui = new ConsoleUI();
        ui.Run();
    }
}