// See https://aka.ms/new-console-template for more information
#pragma warning disable IDE0059 // Unnecessary assignment of a value

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
string s1 = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//string s2 = s1.ToUpper();
//#pragma warning restore CS8602 // Dereference of a possibly null reference.

string? s2 = s1?.ToUpper();
string s3 = s1?.ToUpper() ?? string.Empty;

if (s1 is not null)
{
    string s4 = s1.ToUpper();
}

if (s1 != null)
{
    string s5 = s1.ToUpper();
}

#pragma warning disable CS0219 // Variable is assigned but its value is never used
Book? b1 = null;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
#pragma warning disable IDE0090 // Use 'new(...)'
Book b2 = new Book("Professional C#");
#pragma warning restore IDE0090 // Use 'new(...)'
string title = b2.Title;
string? publisher = b2.Publisher;

#pragma warning disable IDE0040 // Add accessibility modifiers
class Book
#pragma warning restore IDE0040 // Add accessibility modifiers
{
    public Book(string title) => Title = title;

    public string Title { get; set; }
    public string? Publisher { get; set; }
}

#pragma warning restore IDE0059 // Unnecessary assignment of a value
