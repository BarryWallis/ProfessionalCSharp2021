// See https://aka.ms/new-console-template for more information

using ClassesSample;

#pragma warning disable IDE0059 // Unnecessary assignment of a value
Book theBook = new("Professional C#")
{
    Publisher = "Wrox Press"
};
#pragma warning restore IDE0059 // Unnecessary assignment of a value

Person katharina = new("Katharina", "Nagel");
Console.WriteLine($"{katharina.FirstName} {katharina.LastName}");

(string first, string last, int _) = katharina;
Console.WriteLine($"{first} {last}");

katharina.Deconstruct(out first, out last, out int _);
Console.WriteLine($"{first} {last}");
