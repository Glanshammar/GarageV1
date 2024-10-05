namespace Garage_V1.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }

    protected Vehicle(string color, int numberOfWheels, string registrationNumber = null!)
    {
        RegistrationNumber = string.IsNullOrEmpty(registrationNumber) ? RandomRegistrationNumber() : registrationNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Reg.Nr: {RegistrationNumber}, Color: {Color}, Wheels: {NumberOfWheels}";
    }

    private static string RandomRegistrationNumber()
    {
        Random random = new Random();
    
        // Generate 3 random letters
        string randomLetters = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());

        // Generate 3 random single-digit numbers
        string randomNumbers = new string(Enumerable.Repeat("0123456789", 3)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());

        // Combine letters and numbers
        return randomLetters + randomNumbers;
    }
}