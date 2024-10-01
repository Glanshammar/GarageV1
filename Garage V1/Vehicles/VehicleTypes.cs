namespace Garage_V1.Vehicles;

public class Airplane : Vehicle
{
    public int NumberOfEngines { get; set; }

    public Airplane(string color, int numberOfWheels, int numberOfEngines, string registrationNumber = null) 
        : base(color, numberOfWheels, registrationNumber)
    {
        NumberOfEngines = numberOfEngines;
    }

    public override string ToString()
    {
        return base.ToString() + $", Engines: {NumberOfEngines}";
    }
}

public class Motorcycle : Vehicle
{
    public int CylinderVolume { get; set; }

    public Motorcycle(string color, int numberOfWheels, int cylinderVolume, string registrationNumber = null) 
        : base(color, numberOfWheels, registrationNumber)
    {
        CylinderVolume = cylinderVolume;
    }

    public override string ToString()
    {
        return base.ToString() + $", Cylinder Volume: {CylinderVolume}cc";
    }
}

public class Car : Vehicle
{
    public enum FuelType { Petrol, Diesel }
    public FuelType Fuel { get; set; }

    public Car(string color, int numberOfWheels, FuelType fuel, string registrationNumber = null) 
        : base(color, numberOfWheels, registrationNumber)
    {
        Fuel = fuel;
    }

    public override string ToString()
    {
        return base.ToString() + $", Fuel Type: {Fuel}";
    }
}

public class Bus : Vehicle
{
    public int NumberOfSeats { get; set; }

    public Bus(string color, int numberOfWheels, int numberOfSeats, string registrationNumber = null) 
        : base(color, numberOfWheels, registrationNumber)
    {
        NumberOfSeats = numberOfSeats;
    }

    public override string ToString()
    {
        return base.ToString() + $", Seats: {NumberOfSeats}";
    }
}

public class Boat : Vehicle
{
    public double Length { get; set; }

    public Boat(string color, int numberOfWheels, double length, string registrationNumber = null) 
        : base(color, numberOfWheels, registrationNumber)
    {
        Length = length;
    }

    public override string ToString()
    {
        return base.ToString() + $", Length: {Length}m";
    }
}