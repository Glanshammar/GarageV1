namespace Garage_V1.Garage;

public class GarageHandler
{
    private List<Vehicle> _parkedVehicles;
    
    public int Capacity { get; }

    public GarageHandler(int capacity)
    {
        _parkedVehicles = new List<Vehicle>();
        Capacity = capacity;
    }

    public bool ParkVehicle(Vehicle vehicle)
    {
        if (_parkedVehicles.Count >= Capacity)
        {
            return false;
        }
        _parkedVehicles.Add(vehicle);
        return true;
    }

    public bool RetrieveVehicle(string registrationNumber)
    {
        var vehicle = _parkedVehicles.FirstOrDefault(v => v.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        if (vehicle != null)
        {
            _parkedVehicles.Remove(vehicle);
            return true;
        }
        return false;
    }

    public List<Vehicle> GetAllParkedVehicles()
    {
        return _parkedVehicles.ToList();
    }

    public Dictionary<Type, int> GetVehicleTypeCounts()
    {
        return _parkedVehicles.GroupBy(v => v.GetType())
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public List<Vehicle> SearchVehicles(Func<Vehicle, bool> predicate)
    {
        return _parkedVehicles.Where(predicate).ToList();
    }

    public int GetAvailableSpaces()
    {
        return Capacity - _parkedVehicles.Count;
    }
}