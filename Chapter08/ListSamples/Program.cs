// See https://aka.ms/new-console-template for more information

using ListSamples;

Racer graham = new(7, "Gtaham", "Hill", "UK", 14);
Racer emerson = new(13, "Emerson", "Fittipaldi", "Brazil", 14);
Racer mario = new(16, "Mario", "Andretti", "USA", 12);
List<Racer> racers = new()
{
    graham,
    emerson,
    mario,
    new(24, "Michael", "Schumacher", "Germany", 91),
    new(27, "Mike", "Hakkinen", "Finland", 20)
};
racers.AddRange(new Racer[]
{
    new(14, "Niki", "lauda", "Austria", 25),
    new(21, "Alain", "Prost", "France", 51),
});
racers.Insert(3, new(6, "Phil", "Hill", "USA", 3));
Racer r1 = racers[3];

for (int i = 0; i < racers.Count; i++)
{
    Console.WriteLine(racers[i]);
}
Console.WriteLine();

foreach (Racer r in racers)
{
    Console.WriteLine(r);
}
Console.WriteLine();

if (!racers.Remove(graham))
{
    Console.WriteLine("object not found in collection");
    Console.WriteLine();
}

List<Racer> bigWinners = racers.FindAll(r => r.Wins > 20);
foreach (Racer r in bigWinners)
{
    Console.WriteLine($"{r:A}");
}
Console.WriteLine();

racers.Sort(new RacerComparer(RacerComparer.CompareType.Country));
foreach (Racer r in racers)
{
    Console.WriteLine($"{r:A}");
}
Console.WriteLine();

racers.Sort((r1, r2) => r2.Wins.CompareTo(r1.Wins));
foreach (Racer r in racers)
{
    Console.WriteLine($"{r:W}");
}
Console.WriteLine();
