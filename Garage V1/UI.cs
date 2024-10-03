namespace Garage_V1;

public class ConsoleUI : IUI
{
    private List<IHandler> _garages = new List<IHandler>();

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Garage Management System ---");
            Console.WriteLine("1. Create a new garage");
            Console.WriteLine("2. List all vehicles in a garage");
            Console.WriteLine("3. List vehicle types and counts");
            Console.WriteLine("4. Add a vehicle to a garage");
            Console.WriteLine("5. Remove a vehicle from a garage");
            Console.WriteLine("6. Search for vehicles");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        _garages.Add(CreateGarage());
                        break;
                    case 2:
                        ListAllVehicles(SelectGarage());
                        break;
                    case 3:
                        ListVehicleTypes(SelectGarage());
                        break;
                    case 4:
                        AddVehicle(SelectGarage());
                        break;
                    case 5:
                        RemoveVehicle(SelectGarage());
                        break;
                    case 6:
                        SearchVehicles(SelectGarage());
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
    
    public void ListAllVehicles(IHandler garage)
    {
        var vehicles = garage.GetAllParkedVehicles();
        if (vehicles.Count == 0)
        {
            Console.WriteLine("The garage is empty.");
        }
        else
        {
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }
    }

    public void ListVehicleTypes(IHandler garage)
    {
        var typeCounts = garage.GetVehicleTypeCounts();
        foreach (var typeCount in typeCounts)
        {
            Console.WriteLine($"{typeCount.Key.Name}: {typeCount.Value}");
        }
    }

    public void AddVehicle(IHandler handler)
    {
        Console.Write("Vehicle type (Car/Motorcycle/Airplane/Bus/Boat): ");
        string type = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Write("Vehicle color: ");
        string color = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Write("Registration number (optional, random if empty): ");
        string regNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Write("Fuel type: ");
        string fuelTypeInput = Console.ReadLine()?.Trim() ?? string.Empty;

        Car.FuelType fuelType;
        if (fuelTypeInput == "diesel")
            fuelType = Car.FuelType.Diesel;
        else if (fuelTypeInput == "petrol")
            fuelType = Car.FuelType.Petrol;
        else
        {
            Console.WriteLine("Invalid fuel type. Defaulting to Petrol.");
            fuelType = Car.FuelType.Petrol;
        }

        Vehicle vehicle = null;
        switch (type.ToLower())
        {
            case "car":
                vehicle = new Car(color, 4, Car.FuelType.Petrol, regNumber);
                break;
            case "motorcycle":
                vehicle = new Motorcycle(color, 2, 1, regNumber);
                break;
            case "airplane":
                vehicle = new Airplane(color, 3, 2, regNumber);
                break;
            case "bus":
                vehicle = new Bus(color, 4, 25, regNumber);
                break;
            case "boat":
                vehicle = new Boat(color, 0, 5, regNumber);
                break;
            default:
                Console.WriteLine("Invalid vehicle type.");
                return;
        }

        if (handler.ParkVehicle(vehicle))
        {
            Console.WriteLine("Vehicle added successfully.");
            Console.WriteLine(vehicle.ToString());
        }
        else
        {
            Console.WriteLine("Garage is full. Cannot add vehicle.");
        }
    }

    public void RemoveVehicle(IHandler garage)
    {
        Console.Write("Enter registration number of the vehicle to remove: ");
        string regNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        
        if (garage.RetrieveVehicle(regNumber))
        {
            Console.WriteLine($"Vehicle removed successfully: {regNumber}");
        }
        else
        {
            Console.WriteLine("Vehicle not found in the garage.");
        }
    }

    public void SearchVehicles(IHandler garage)
    {
        Console.Write("Enter search property (Color/Brand/Model): ");
        string property = Console.ReadLine()?.ToLower().Trim() ?? string.Empty;
        Console.Write("Enter search value: ");
        string value = Console.ReadLine()?.ToLower().Trim() ?? string.Empty;

        Func<Vehicle, bool> predicate = vehicle =>
        {
            switch (property)
            {
                case "color":
                    return vehicle.Color.ToLower() == value;
                case "brand":
                    return vehicle.Brand.ToLower() == value;
                case "model":
                    return vehicle.Model.ToLower() == value;
                default:
                    return false;
            }
        };

        var results = garage.SearchVehicles(predicate);
        if (results.Count == 0)
        {
            Console.WriteLine("No vehicles found matching the search criteria.");
        }
        else
        {
            foreach (var vehicle in results)
            {
                Console.WriteLine(vehicle);
            }
        }
    }

    public IHandler CreateGarage()
    {
        Console.Write("Enter garage capacity: ");
        if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0)
        {
            var garage = new GarageHandler(capacity);
            Console.WriteLine($"New garage created with capacity {capacity}.");
            return garage;
        }
        else
        {
            Console.WriteLine("Invalid capacity. Creating a default garage with capacity 10.");
            return new GarageHandler(10);
        }
    }

    private IHandler SelectGarage()
    {
        if (_garages.Count == 0)
        {
            Console.WriteLine("No garages available. Creating a new garage.");
            return CreateGarage();
        }
        else if (_garages.Count == 1)
        {
            return _garages[0];
        }
        else
        {
            Console.WriteLine("Select a garage:");
            for (int i = 0; i < _garages.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Garage (Capacity: {_garages[i].Capacity})");
            }
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _garages.Count)
            {
                return _garages[choice - 1];
            }
            else
            {
                Console.WriteLine("Invalid choice. Using the first garage.");
                return _garages[0];
            }
        }
    }
}