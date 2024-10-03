namespace Garage_V1.Interfaces;

public interface IUI
{
    public void Run();
    public void ListAllVehicles(IHandler handler);
    public void ListVehicleTypes(IHandler handler);
    public void AddVehicle(IHandler handler);
    public void RemoveVehicle(IHandler handler);
    public void SearchVehicles(IHandler handler);
    public IHandler CreateGarage();
}