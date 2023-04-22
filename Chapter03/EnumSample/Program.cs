// See https://aka.ms/new-console-template for more information

ColorSamples();
Console.WriteLine();

static void ColorSamples()
{
    Color c1 = Color.Red;
    Console.WriteLine(c1);
    Console.WriteLine();

    DaysOfWeek mondayAndWednesday = DaysOfWeek.Monday | DaysOfWeek.Wednesday;
    Console.WriteLine(mondayAndWednesday);
    Console.WriteLine();

    DaysOfWeek weekend = DaysOfWeek.Saturday | DaysOfWeek.Sunday;
    Console.WriteLine(weekend);
    Console.WriteLine();

    if (Enum.TryParse<Color>("Red", out Color red))
    {
        Console.WriteLine($"successfully parsed {red}");
    }
    Console.WriteLine();

    foreach (short color in Enum.GetValues(typeof(Color)))
    {
        Console.WriteLine(color);
    }
    Console.WriteLine();
}
