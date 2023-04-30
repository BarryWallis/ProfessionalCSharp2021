// See https://aka.ms/new-console-template for more information

SortedList<string, string> books = new()
{
    { "Front-end Development with ASP.Net Core", "976-1-119-18140-8" },
    { "Beginning C# 7 Progrmming", "978-1-119-45866-1" }
};

books["Enterprise Services"] = "978-0321246738";
books["Professional C# 7 and .Net Core 2.1"] = "978-119-44926-3";
foreach (KeyValuePair<string, string> book in books)
{
    Console.WriteLine($"{book.Key}, {book.Value}");
}
