using YieldSample;

HelloWorld();

MusicTitles titles = new();
foreach (string title in titles)
{
    Console.WriteLine(title);
}
Console.WriteLine();

Console.WriteLine("reverse");
foreach (string title in titles.Reverse())
{
    Console.WriteLine(title);
}
Console.WriteLine();

Console.WriteLine("subset");
foreach (string title in titles.Subset(2, 2))
{
    Console.WriteLine(title);
}
Console.WriteLine();

static void HelloWorld()
{
    HelloCollection helloCollection = new();
    foreach (string s in helloCollection)
    {
        Console.WriteLine(s);
    }
    Console.WriteLine();
}

#pragma warning disable CA1050 // Declare types in namespaces
public class HelloCollection
#pragma warning restore CA1050 // Declare types in namespaces
{
#pragma warning disable CA1822 // Mark members as static
    public IEnumerator<string> GetEnumerator()
#pragma warning restore CA1822 // Mark members as static
    {
        yield return "Hello";
        yield return "World";
    }
}
