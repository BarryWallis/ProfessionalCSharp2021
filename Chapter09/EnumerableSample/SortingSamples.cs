using DataLib;

namespace EnumerableSample;
public static class SortingSamples
{
    public static void SortDescending()
    {
        IOrderedEnumerable<Racer> racers = from racer in Formula1.GetChampions()
                                           where racer.Country == "Brazil"
                                           orderby racer.Wins descending
                                           select racer;
        foreach (Racer racer in racers)
        {
            Console.WriteLine($"{racer:A}");
        }
        Console.WriteLine();
    }

    public static void SortDescendingWithMethods()
    {
        IEnumerable<Racer> racers = Formula1.GetChampions()
                                            .Where(r => r.Country == "Brazil")
                                            .OrderByDescending(r => r.Wins)
                                            .Select(r => r);
        foreach (Racer racer in racers)
        {
            Console.WriteLine($"{racer:A}");
        }
        Console.WriteLine();
    }

    public static void SortMultiple()
    {
        IEnumerable<Racer> racers = (from racer in Formula1.GetChampions()
                                     orderby racer.Country, racer.LastName, racer.FirstName
                                     select racer).Take(10);
        foreach (Racer racer in racers)
        {
            Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
        }
        Console.WriteLine();
    }

    public static void SortMultipleWithMethods()
    {
        IEnumerable<Racer> racers = Formula1.GetChampions()
                                            .OrderBy(r => r.Country)
                                            .ThenBy(r => r.LastName)
                                            .ThenBy(r => r.FirstName)
                                            .Take(10);
        foreach (Racer racer in racers)
        {
            Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
        }
        Console.WriteLine();
    }
}
