using DataLib;

namespace EnumerableSample;
public class JoinSamples
{
    public static void InnerJoin()
    {
        var racers = from r in Formula1.GetChampions()
                     from y in r.Years
                     select new { Year = y, Name = $"{r.FirstName} {r.LastName}" };
        var teams = from t in Formula1.GetConstructorChampions()
                    from y in t.Years
                    select new { Year = y, t.Name };
        var racersAndTeams = (from r in racers
                              join t in teams on r.Year equals t.Year
                              orderby r.Year
                              select new { r.Year, Champion = r.Name, Constructor = t.Name }
                             ).Take(10);
        Console.WriteLine($"Year  World Champion       Constructor Title");
        foreach (var item in racersAndTeams)
        {
            Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
        }
        Console.WriteLine();
    }

    public static void InnerJoinWithMethods()
    {
        var racers = Formula1.GetChampions()
                             .SelectMany(r => r.Years, (r1, year)
                                                => new { Year = year, Name = $"{r1.FirstName} {r1.LastName}" });
        var teams = Formula1.GetConstructorChampions()
                            .SelectMany(t => t.Years, (t, year)
                                              => new { Year = year, t.Name });
        var racersAndTeams = racers.Join(teams, r => r.Year, t
                                                                         => t.Year, (
                                                                         r,
                                                                         t)
                                                                         => new
                                                                         {
                                                                             r.Year,
                                                                             Champion = r.Name,
                                                                             Constructor = t.Name
                                                                         })
                                   .OrderBy(item => item.Year)
                                   .Take(10);
        Console.WriteLine($"Year  World Champion       Constructor Title");
        foreach (var item in racersAndTeams)
        {
            Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
        }
        Console.WriteLine();
    }

    public static void LeftOuterJoin()
    {
        var racers = from r in Formula1.GetChampions()
                     from y in r.Years
                     select new { Year = y, Name = $"{r.FirstName} {r.LastName}" };
        var teams = from t in Formula1.GetConstructorChampions()
                    from y in t.Years
                    select new { Year = y, t.Name };
        var racersAndTeams = (from r in racers
                              join t in teams on r.Year equals t.Year into rt
                              from t in rt.DefaultIfEmpty()
                              orderby r.Year
                              select new
                              {
                                  r.Year,
                                  Champion = r.Name,
                                  Constructor = t is null ? "no constructor championship" : t.Name
                              }).Take(10);
        Console.WriteLine($"Year  World Champion       Constructor Title");
        foreach (var item in racersAndTeams)
        {
            Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
        }
        Console.WriteLine();
    }

    public static void LeftOuterJoinWithMethods()
    {
        var racers = Formula1.GetChampions()
                             .SelectMany(r => r.Years, (r1, year)
                                                => new { Year = year, Name = $"{r1.FirstName} {r1.LastName}" });
        var teams = Formula1.GetConstructorChampions()
                            .SelectMany(t => t.Years, (t, year)
                                              => new { Year = year, t.Name });
        var racersAndTeams = racers.GroupJoin(teams,
                                              r => r.Year,
                                              t => t.Year,
                                              (r, ts) => new
                                              {
                                                  r.Year,
                                                  Champion = r.Name,
                                                  Constructors = ts
                                              })
                                    .SelectMany(
                                        rt => rt.Constructors.DefaultIfEmpty(),
                                        (r, t) => new
                                        {
                                            r.Year,
                                            r.Champion,
                                            Constructor = t?.Name ?? "no constructor championship"
                                        }).Take(10);
        Console.WriteLine($"Year  World Champion       Constructor Title");
        foreach (var item in racersAndTeams)
        {
            Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
        }
        Console.WriteLine();
    }

    public static void GroupJoin()
    {
        IEnumerable<(int Year, int Position, string FirstName, string LastName)> racers
            = from cs in Formula1.GetChampionships()
              from r in new List<(int Year, int Position, string FirstName, string LastName)>()
                {
                    (cs.Year, Position: 1, FirstName: cs.First.FirstName(), LastName: cs.First.LasttName()),
                    (cs.Year, Position: 2, FirstName: cs.Second.FirstName(), LastName: cs.Second.LasttName()),
                    (cs.Year, Position: 3, FirstName: cs.Third.FirstName(), LastName: cs.Third.LasttName()),
                }
              select r;
        IEnumerable<(string FirstName, string LastName, int Wins, int Starts,
                     IEnumerable<(int Year, int Position, string FirstName, string LastName)> Results)> q
            = from r in Formula1.GetChampions()
              join r2 in racers on (r.FirstName, r.LastName) equals (r2.FirstName, r2.LastName)
              into yearResults
              select (r.FirstName, r.LastName, r.Wins, r.Starts, Results: yearResults);

        foreach ((string FirstName, string LastName, int Wins, int Starts, IEnumerable<(int Year, int Position,
                  string FirstName, string LastName)> Results
                 ) r in q)
        {
            Console.WriteLine($"{r.FirstName} {r.LastName}");
            foreach ((int Year, int Position, string FirstName, string LastName) in r.Results)
            {
                Console.WriteLine($"\t{Year} {Position}");
            }
        }
    }

    public static void GroupJoinWithMethods()
    {
        IEnumerable<(int Year, int Position, string FirstName, string LastName)> racers = Formula1.GetChampionships()
                             .SelectMany(cs
                                => new List<(int Year, int Position, string FirstName, string LastName)>
                                {
                                    (cs.Year, Position: 1, cs.First.FirstName(), cs.First.LasttName()),
                                    (cs.Year, Position: 2, cs.Second.FirstName(), cs.Second.LasttName()),
                                    (cs.Year, Position: 3, cs.Third.FirstName(), cs.Third.LasttName()),
                                });
        IEnumerable<(string FirstName, string LastName, int Wins, int Starts,
                     IEnumerable<(int Year, int Position, string FirstName, string LastName)> Results)> q
            = Formula1.GetChampions()
                      .GroupJoin(racers,
                                 r1 => (r1.FirstName, r1.LastName),
                                 r2 => (r2.FirstName, r2.LastName),
                                 (r1, r2s) => (r1.FirstName, r1.LastName, r1.Wins, r1.Starts, Results: r2s));
        foreach ((string FirstName, string LastName, int Wins, int Starts, IEnumerable<(int Year, int Position, string FirstName, string LastName)> Results) r in q)
        {
            Console.WriteLine($"{r.FirstName} {r.LastName}");
            foreach ((int Year, int Position, string FirstName, string LastName) in r.Results)
            {
                Console.WriteLine($"\t{Year} {Position}");
            }
        }
        Console.WriteLine();
    }
}
