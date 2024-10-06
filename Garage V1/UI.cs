namespace Garage_V1;

public class ConsoleUI : IUI
{
    private List<IHandler> _garages = new List<IHandler>();

    public static void ShowMenu()
    {
        Console.WriteLine("--- Garage Management System ---");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Create a new garage");
        Console.WriteLine("2. List all vehicles in a garage");
        Console.WriteLine("3. List vehicle types and counts");
        Console.WriteLine("4. Add a vehicle to a garage");
        Console.WriteLine("5. Remove a vehicle from a garage");
        Console.WriteLine("6. Search for vehicles");
    }
    
    public void Run()
    {
        ShowMenu();
        
        while (true)
        {
            Console.Write(">> ");
            if (byte.TryParse(Console.ReadLine(), out byte choice))
            {
                switch (choice)
                {
                    case 0:
                        return;
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
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        ShowMenu();
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
        Console.WriteLine("--- Vehicle Types ---");
        var typeCounts = garage.GetVehicleTypeCounts();
        
        if (typeCounts.Count == 0)
            Console.WriteLine("No vehicle types found.");
        
        foreach (var typeCount in typeCounts)
            Console.WriteLine($"{typeCount.Key.Name}: {typeCount.Value}");
    }

    public void AddVehicle(IHandler garage)
    {
        List<string> colorList = new List<string>
        {
            "Red", "Green", "Blue", "Yellow", "Black", "White", "Pink",
            "Cyan", "Magenta", "Orange", "Purple", "Brown", "Gray", "Crimson"
        };
        
        Console.Write("Vehicle type (Car/Motorcycle/Airplane/Bus/Boat): ");
        string type = Console.ReadLine()?.Trim() ?? string.Empty;
        
        string color;
        while (true)
        {
            Console.Write("Vehicle color: ");
            color = Console.ReadLine()?.Trim() ?? string.Empty;
            color = char.ToUpper(color[0]) + color.Substring(1).ToLower(); // Capitalize the first letter, lowercase the rest
        
            if (colorList.Contains(color))
            {
                break; // Valid color, exit the loop
            }
            else
            {
                Console.WriteLine("Invalid color. Please choose from the following:");
                Console.WriteLine(string.Join(", ", colorList));
            }
        }
        
        Console.Write("Registration number (optional, random if empty): ");
        string regNumber = Console.ReadLine()?.Trim() ?? string.Empty;
        
        Console.Write("Fuel type (petrol/diesel): ");
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

        if (garage.ParkVehicle(vehicle))
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
        Console.WriteLine("--- Search Vehicles After Properties ---"
                          + "\n(Color, Brand, Model"
                          );
        Console.Write("Enter search value: ");
        string value = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

        Func<Vehicle, bool> predicate = vehicle =>
            (vehicle.Color?.ToLower().Contains(value) ?? false) ||
            (vehicle.Brand?.ToLower().Contains(value) ?? false) ||
            (vehicle.Model?.ToLower().Contains(value) ?? false);

        try
        {
            var results = garage.SearchVehicles(predicate);
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("No vehicles found matching the search criteria.");
            }
            else
            {
                Console.WriteLine($"Found {results.Count} matching vehicles:");
                foreach (var vehicle in results)
                {
                    Console.WriteLine(vehicle);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while searching: {ex.Message}");
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
            _garages.Add(CreateGarage());
            return _garages[0];
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