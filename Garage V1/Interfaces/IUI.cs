namespace Garage_V1.Interfaces;

public interface IUI
{
    void Run();
    void ListAllVehicles(IHandler handler);
    void ListVehicleTypes(IHandler handler);
    void AddVehicle(IHandler handler);
    void RemoveVehicle(IHandler handler);
    void SearchVehicles(IHandler handler);
    IHandler CreateHandler();
}