// See https://aka.ms/new-console-template for more information

using System.Dynamic;
using System.Globalization;

using DynamicSample;

UseExpando();

static void UseExpando()
{
    dynamic expandoObject = new ExpandoObject();
    expandoObject.FirstName = "Daffy";
    expandoObject.LastName = "Duck";
    Console.WriteLine($"{expandoObject.FirstName} {expandoObject.LastName}");

    expandoObject.GetNextDay = new Func<DateTime, string>(day => day.AddDays(1).ToString());
    Console.WriteLine($"next day: {expandoObject.GetNextDay(new DateTime(2021, 1, 3))}");

    expandoObject.Friends = new List<Person>();
    expandoObject.Friends.Add(new Person("Bob", "Jones"));
    expandoObject.Friends.Add(new Person("Robert", "Jones"));
    expandoObject.Friends.Add(new Person("Bobby", "Jones"));

    foreach (dynamic friend in expandoObject.Friends)
    {
        Console.WriteLine($"{friend.FirstName} {friend.LastName}");
    }
    Console.WriteLine();
}
