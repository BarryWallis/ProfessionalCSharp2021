// See https://aka.ms/new-console-template for more information

Book2 b2 = new("Professional C#", "Wrox Press");
Console.WriteLine(b2);

Book1 book1a = new() { Title = "Professional C#", Publisher = "Wrox Press" };
Book1 book1b = new() { Title = "Professional C#", Publisher = "Wrox Press" };
if (!object.ReferenceEquals(book1a, book1b))
{
    Console.WriteLine("Two different references for equal records");
}

if (book1a == book1b)
{
    Console.WriteLine("Both records have the same values");
}

Console.WriteLine();

#pragma warning disable CA1050 // Declare types in namespaces
public record Book1
{
    public string Title { get; init; } = string.Empty;
    public string Publisher { get; init; } = string.Empty;
}

public record Book2(string Title, string Publisher);
#pragma warning restore CA1050 // Declare types in namespaces
