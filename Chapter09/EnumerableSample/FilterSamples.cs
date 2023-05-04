using DataLib;

namespace EnumerableSample;

public static class FilterSamples
{
    public static void SimpleFilter()
    {
        IEnumerable<Racer> racers = from r in Formula1.GetChampions()
                                    where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                                    select r;
        foreach (Racer r in racers)
        {
            Console.WriteLine($"{r:A}");
        }
        Console.WriteLine();
    }

    public static void FilterWithMethods()
    {
        IEnumerable<Racer> racers = Formula1.GetChampions()
                                            .Where(r => r.Wins > 15 && (r.Country is "Brazil" or "Austria"));
    }

    public static void FilteringWithIndex()
    {
        IEnumerable<Racer> racers = Formula1.GetChampions()
                                            .Where((r, index) => r.LastName.StartsWith("A")
                                                                         && index % 2 != 0);
        foreach (Racer r in racers)
        {
            Console.WriteLine($"{r:A}");
        }
        Console.WriteLine();
    }

    public static void TypeFilter()
    {
        object[] data = { "one", 2, 3, "four", "five", 6, };
        IEnumerable<string> query = data.OfType<string>();
        foreach (string s in query)
        {
            Console.WriteLine(s);
        }
        Console.WriteLine();
    }
}
