// See https://aka.ms/new-console-template for more information

int? n1 = null;
if (n1.HasValue)
{
#pragma warning disable IDE0059 // Unnecessary assignment of a value
    int n2 = n1.Value;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
}
int n3 = 42;
#pragma warning disable IDE0059 // Unnecessary assignment of a value
int? n4 = n3;
#pragma warning restore IDE0059 // Unnecessary assignment of a value

#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0219 // Variable is assigned but its value is never used
Book? b1 = null;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0090 // Use 'new(...)'
Book b2 = new Book("Professional C#");
#pragma warning restore IDE0090 // Use 'new(...)'
#pragma warning disable IDE0059 // Unnecessary assignment of a value
string title = b2.Title;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0059 // Unnecessary assignment of a value
string? publisher = b2.Publisher;
#pragma warning restore IDE0059 // Unnecessary assignment of a value

#pragma warning disable IDE0040 // Add accessibility modifiers
class Book
#pragma warning restore IDE0040 // Add accessibility modifiers
{
    public Book(string title) => Title = title;

    public string Title { get; set; }
    public string? Publisher { get; set; }
}
