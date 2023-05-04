using System.Collections;

using DataLib;

namespace EnumerableSample;
public static class LinqSamples
{
    public static void SetOperations()
    {
        Console.WriteLine(nameof(SetOperations));
        IEnumerable<Racer> racersByCar(string car) => from r in Formula1.GetChampions()
                                                      from c in r.Cars
                                                      where c == car
                                                      orderby r.LastName
                                                      select r;
        Console.WriteLine($"World champion with Ferrari and McLaren");
        foreach (Racer racer in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
        {
            Console.WriteLine(racer);
        }
        Console.WriteLine();
    }

    public static void ZipOperations()
    {
        Console.WriteLine(nameof(ZipOperations));
        var racerNames = from r in Formula1.GetChampions()
                         where r.Country == "Italy"
                         orderby r.Wins descending
                         select new { Name = $"{r.FirstName} {r.LastName}" };
        var racerNamesAndStarts = from r in Formula1.GetChampions()
                                  where r.Country == "Italy"
                                  orderby r.Wins descending
                                  select new { r.LastName, r.Starts };
        IEnumerable<string> racers
            = racerNames.Zip(racerNamesAndStarts, (first, second) => $"{first.Name}, starts: {second.Starts}");

        foreach (string r in racers)
        {
            Console.WriteLine(r);
        }
        Console.WriteLine();
    }

    public static void Partitioning()
    {
        Console.WriteLine(nameof(Partitioning));
        int pageSize = 5;
        int numberOfPages = (int)Math.Ceiling(Formula1.GetChampions().Count / (double)pageSize);

        for (int page = 0; page < numberOfPages; page++)
        {
            Console.WriteLine($"Page {page}");
            IEnumerable<string> racers = (from r in Formula1.GetChampions()
                                          orderby r.LastName, r.FirstName
                                          select $"{r.FirstName} {r.LastName}"
                                         ).Skip(page * pageSize).Take(pageSize);
            foreach (string name in racers)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void AggregateCount()
    {
        Console.WriteLine(nameof(AggregateCount));
        var query = from r in Formula1.GetChampions()
                    let numberOfYears = r.Years.Count()
                    where numberOfYears >= 3
                    orderby numberOfYears descending, r.LastName
                    select new
                    {
                        Name = $"{r.FirstName} {r.LastName}",
                        TimesChampion = numberOfYears,
                    };

        foreach (var r in query)
        {
            Console.WriteLine($"{r.Name} {r.TimesChampion}");
        }
        Console.WriteLine();
    }

    public static void AggregateSum()
    {
        Console.WriteLine(nameof(AggregateSum));
        var countries = (from c in
                             from r in Formula1.GetChampions()
                             group r by r.Country into c
                             select new
                             {
                                 Country = c.Key,
                                 Wins = (from r1 in c select r1.Wins).Sum()
                             }
                         orderby c.Wins descending, c.Country
                         select c).Take(5);
        foreach (var country in countries)
        {
            Console.WriteLine($"{country.Country} {country.Wins}");
        }
        Console.WriteLine();
    }

    public static void ToList()
    {
        Console.WriteLine(nameof(ToList));
        List<Racer> racers = (from r in Formula1.GetChampions()
                              where r.Starts > 220
                              orderby r.Starts descending
                              select r
                             ).ToList();
        foreach (Racer racer in racers)
        {
            Console.WriteLine($"{racer} {racer:S}");
        }
        Console.WriteLine();
    }

    public static void ToLookup()
    {
        Console.WriteLine(nameof(ToLookup));
        ILookup<string, Racer> racers = (from r in Formula1.GetChampions()
                                         from c in r.Cars
                                         select new { Car = c, Racer = r }
                                        ).ToLookup(cr => cr.Car, cr => cr.Racer);
        foreach (Racer williamsRacer in racers["Williams"])
        {
            Console.WriteLine(williamsRacer);
        }
        Console.WriteLine();
    }

    public static void ConvertWithCast()
    {
        Console.WriteLine(nameof(ConvertWithCast));
        ArrayList list = new(Formula1.GetChampions() as ICollection ?? throw new InvalidCastException());
        IOrderedEnumerable<Racer> query = from r in list.Cast<Racer>()
                                          where r.Country == "USA"
                                          orderby r.Wins descending
                                          select r;
        foreach (Racer racer in query)
        {
            Console.WriteLine($"{racer:A}");
        }
        Console.WriteLine();
    }

    public static void GenerateRange()
    {
        Console.WriteLine(nameof(GenerateRange));
        IEnumerable<int> values = Enumerable.Range(1, 20);
        foreach (int item in values)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
        Console.WriteLine();
    }
}
