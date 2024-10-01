namespace Garage_V1.Interfaces;

public interface IHandler
{
    int Capacity { get; }
    bool ParkVehicle(Vehicle vehicle);
    bool RetrieveVehicle(string registrationNumber);
    List<Vehicle> GetAllParkedVehicles();
    Dictionary<Type, int> GetVehicleTypeCounts();
    List<Vehicle> SearchVehicles(Func<Vehicle, bool> predicate);
    int GetAvailableSpaces();
}