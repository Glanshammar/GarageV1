namespace Garage_V1.Interfaces;

public interface IVehicle
{
    string RegistrationNumber { get; set; } 
    string Color { get; set; }
    string Brand { get; set; }
    int NumberOfWheels { get; set; }
}