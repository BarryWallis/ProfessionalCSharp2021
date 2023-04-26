// See https://aka.ms/new-console-template for more information

using EqualitySample;

Book book1 = new("Professional C#", "Wrox Press");
Book book2 = new("Professional C#", "Wrox Press");

if (!object.ReferenceEquals(book1, book2))
{
    Console.WriteLine("Not the same reference");
}

if (book1.Equals(book2))
{
    Console.WriteLine("The same object using the generic equals method");
}

object book3 = book2;
if (book1.Equals(book3))
{
    Console.WriteLine("The same object using the overridden equals method");
}

if (book1 == book2)
{
    Console.WriteLine("The same book using the == operator");
}
