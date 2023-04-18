// See https://aka.ms/new-console-template for more information

Console.WriteLine("Type in a string");
string? input = Console.ReadLine();
if (string.IsNullOrEmpty(input))
{
    Console.WriteLine("You typed in an empty string.");
}
else if (input?.Length < 5)
{
    Console.WriteLine("The string had less than 5 characters.");
}
else
{
    Console.WriteLine("Read any other string.");
}
Console.WriteLine("The string was " + input);

#pragma warning disable CS8321 // Local function is declared but never used
#pragma warning disable IDE0062 // Make local function 'static'
void PatternMatching(object o)
{
#pragma warning disable IDE0011 // Add braces
    if (o is null) throw new ArgumentNullException(nameof(o));
#pragma warning restore IDE0011 // Add braces
    else if (o is Book b)
    {
        Console.WriteLine($"received a book: {b.Title}");
    }
}
#pragma warning restore IDE0062 // Make local function 'static'
#pragma warning restore CS8321 // Local function is declared but never used

#pragma warning disable IDE0040 // Add accessibility modifiers
class Book
#pragma warning restore IDE0040 // Add accessibility modifiers
{
#pragma warning disable IDE1006 // Naming Styles
    public string Title;
    public string? Publisher;
#pragma warning restore IDE1006 // Naming Styles

    public Book(string title, string? publisher)
    {
        Title = title;
        Publisher = publisher;
    }
}
