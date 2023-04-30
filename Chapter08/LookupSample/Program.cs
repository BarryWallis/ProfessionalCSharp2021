// See https://aka.ms/new-console-template for more information

using LookupSample;

List<Racer> racers = new()
{
    new(26, "Jacques", "Villeneuve", "Canada", 11),
    new(18, "Alan", "Jones", "Australia", 12),
    new(11, "Jackie", "Stewart", "United Kingdom", 27),
    new(15, "James", "Hunt", "UNited Kingdom", 10),
    new(5, "Jack", "Brabham", "Australia", 14),
};

ILookup<string, Racer> lookupRacers = racers.ToLookup(r => r.Country);
foreach (Racer racer in lookupRacers["Australia"])
{
    Console.WriteLine(racer);
}
Console.WriteLine();
