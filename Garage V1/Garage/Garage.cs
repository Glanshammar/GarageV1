namespace Garage_V1.Garage;

public class Garage<T> : IEnumerable<T> where T : IVehicle
{
    private T[] vehicles;
    private int count;

    public Garage(int capacity)
    {
        vehicles = new T[capacity];
        count = 0;
    }

    public bool AddVehicle(T vehicle)
    {
        if (count < vehicles.Length)
        {
            vehicles[count] = vehicle;
            count++;
            return true;
        }
        return false;
    }

    public bool RemoveVehicle(T vehicle)
    {
        for (int i = 0; i < count; i++)
        {
            if (vehicles[i].Equals(vehicle))
            {
                for (int j = i; j < count - 1; j++)
                {
                    vehicles[j] = vehicles[j + 1];
                }
                vehicles[count - 1] = default(T);
                count--;
                return true;
            }
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return vehicles[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Capacity => vehicles.Length;
    public int Count => count;
}