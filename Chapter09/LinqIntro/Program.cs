// See https://aka.ms/new-console-template for more information
using DataLib;

LinqQuery();
ExtensionMethods();
DeferredQuery();
ImmediateQuery();

static void LinqQuery()
{
    IOrderedEnumerable<Racer> query = from r in Formula1.GetChampions()
                                      where r.Country == "Brazil"
                                      orderby r.Wins descending
                                      select r;

    foreach (Racer racer in query)
    {
        Console.WriteLine($"{racer:A}");
    }
    Console.WriteLine();
}

static void ExtensionMethods()
{
    List<Racer> champions = new(Formula1.GetChampions());
    IEnumerable<Racer> braxilChampions = champions.Where(r => r.Country == "Brazil")
                                                  .OrderByDescending(r => r.Wins)
                                                  .Select(r => r);

    foreach (Racer racer in braxilChampions)
    {
        Console.WriteLine($"{racer:A}");
    }
    Console.WriteLine();
}

static void DeferredQuery()
{
    List<string> names = new() { "Nino", "Alberto", "Juan", "Mike", "Phil" };
    IOrderedEnumerable<string> namesWithJ = from n in names
                                            where n.StartsWith("J")
                                            orderby n
                                            select n;

    Console.WriteLine("First iteration");
    foreach (string name in namesWithJ)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine();

    names.Add("John");
    names.Add("Jim");
    names.Add("Jack");
    names.Add("Denny");
    Console.WriteLine("Second iteration");
    foreach (string name in namesWithJ)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine();
}

static void ImmediateQuery()
{
    List<string> names = new() { "Nino", "Alberto", "Juan", "Mike", "Phil" };
    List<string> namesWithJ = (from n in names
                               where n.StartsWith("J")
                               orderby n
                               select n).ToList();

    Console.WriteLine("First iteration");
    foreach (string name in namesWithJ)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine();

    names.Add("John");
    names.Add("Jim");
    names.Add("Jack");
    names.Add("Denny");
    Console.WriteLine("Second iteration");
    foreach (string name in namesWithJ)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine();
}
