// See https://aka.ms/new-console-template for more information

#pragma warning disable CS8321 // Local function is declared but never used
#pragma warning disable IDE0062 // Make local function 'static'
void SwitchSample(int x)
{
    switch (x)
    {
        case 1:
            Console.WriteLine("integerA = 1");
            break;
        case 2:
            Console.WriteLine("integerA = 2");
            break;
        case 3:
            Console.WriteLine("integerA = 3");
            break;
        default:
            Console.WriteLine("integerA is not 1, 2, or 3");
            break;

    }
}

void SwitchWithPatternMatching(object o)
{
    switch (o)
    {
        case null:
            Console.WriteLine("const pattern with null");
            break;
        case int i when i > 42:
            Console.WriteLine("type pattern with when and a relational pattern");
            break;
        case int:
            Console.WriteLine("type pattern with an int");
            break;
        case Book b:
            Console.WriteLine($"type pattern with a Book {b.Title}");
            break;
        default:
            break;
    }
}

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

#pragma warning restore IDE0062 // Make local function 'static'
#pragma warning restore CS8321 // Local function is declared but never used
