using DataLib;

namespace EnumerableSample;
public static class CompoundFromSamples
{
    public static void CompoundFrom()
    {
        IEnumerable<string> ferrariDrivers = from racer in Formula1.GetChampions()
                                             from car in racer.Cars
                                             where car == "Ferrari"
                                             orderby racer.LastName
                                             select $"{racer.FirstName} {racer.LastName}";
        foreach (string drivers in ferrariDrivers)
        {
            Console.WriteLine(drivers);
        }
        Console.WriteLine();
    }

    public static void CompoundFromMethods()
    {
        IEnumerable<string> ferrariDrivers = Formula1.GetChampions()
            .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
            .Where(r => r.Car == "Ferrari")
            .OrderBy(r => r.Racer.LastName)
            .Select(r => $"{r.Racer.FirstName} {r.Racer.LastName}");
        foreach (string drivers in ferrariDrivers)
        {
            Console.WriteLine(drivers);
        }
        Console.WriteLine();
    }
}
