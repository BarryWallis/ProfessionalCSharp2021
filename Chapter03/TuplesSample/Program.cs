// See https://aka.ms/new-console-template for more information

(string AString, int Number, Book Book) tuple1 = ("magic", 42, new Book("Professional C#", "Wrox Press"));
Console.WriteLine($"a string: {tuple1.AString}, number: {tuple1.Number}, book: {tuple1.Book}");

#pragma warning disable IDE0008 // Use explicit type
var tuple2 = ("magic", 42, new Book("Professional C#", "Wrox Press"));
#pragma warning restore IDE0008 // Use explicit type
Console.WriteLine($"a string: {tuple2.Item1}, number: {tuple2.Item2}, book: {tuple2.Item3}");

#pragma warning disable IDE0008 // Use explicit type
var tuple3 = (AString: "magic", Number: 42, Book: new Book("Professional C#", "Wrox Press"));
#pragma warning restore IDE0008 // Use explicit type
Console.WriteLine($"a string: {tuple3.AString}, number: {tuple3.Number}, book: {tuple3.Book}");

#pragma warning disable IDE0042 // Deconstruct variable declaration
(string S, int N, Book B) tuple4 = tuple3;
#pragma warning restore IDE0042 // Deconstruct variable declaration
Console.WriteLine($"a string: {tuple4.S}, number: {tuple4.N}, book: {tuple4.B}");

#pragma warning disable IDE0008 // Use explicit type
var tuple5 = (ANumber: 42, new Book("Professional C#", "Wrox Press").Title);
#pragma warning restore IDE0008 // Use explicit type
Console.WriteLine($"a string: {tuple5.ANumber}, book title: {tuple5.Title}");

TuplesDeconstruction();
ReturningTuples();

static void TuplesDeconstruction()
{
#pragma warning disable IDE0008 // Use explicit type
    var tuple1 = (AString: "magic", Number: 42, Book: new Book("Professional C#", "Wrox Press"));
#pragma warning restore IDE0008 // Use explicit type
    (string aString, int number, Book book) = tuple1;
    Console.WriteLine($"a string: {aString}, number: {number}, book: {book}");

#pragma warning disable IDE0008 // Use explicit type
    (_, _, var book1) = tuple1;
#pragma warning restore IDE0008 // Use explicit type
    Console.WriteLine(book1.Title);
}

static (int result, int remainder) Divide(int dividend, int divisor)
{
    int result = dividend / divisor;
    int remainder = dividend % divisor;
    return (result, remainder);
}

static void ReturningTuples()
{
    (int result, int remainder) = Divide(7, 2);
    Console.WriteLine($"7 / 2 - result: {result}, remainder: {remainder}");
}

#pragma warning disable CA1050 // Declare types in namespaces
public record Book(string Title, string Publisher);
#pragma warning restore CA1050 // Declare types in namespaces
