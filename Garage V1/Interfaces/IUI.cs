namespace Garage_V1.Interfaces;

public interface IUI
{
    void Run();
    void ListAllVehicles(GarageHandler garage);
    void ListVehicleTypes(GarageHandler garage);
    void AddVehicle(GarageHandler garage);
    void RemoveVehicle(GarageHandler garage);
    void SearchVehicles(GarageHandler garage);
    GarageHandler CreateGarage();
}