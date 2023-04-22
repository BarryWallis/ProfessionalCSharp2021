// See https://aka.ms/new-console-template for more information
using System;

using GenericTypes;

LinkedList<int> list1 = new();
list1.AddLast(1);
list1.AddLast(3);
list1.AddLast(2);

foreach (int item in list1)
{
    Console.WriteLine(item);
}
Console.WriteLine();

LinkedList<string> list2 = new();
list2.AddLast("two");
list2.AddLast("four");
list2.AddLast("six");
Console.WriteLine(list2.Last);
Console.WriteLine();

LinkedList<(int, int)> list3 = new();
list3.AddLast((1, 2));
list3.AddLast((3, 4));
foreach ((int, int) item in list3)
{
    Console.WriteLine(item);
}
Console.WriteLine();

LinkedList<Person> list4 = new();
list4.AddLast(new Person("Stephanie", "Nagel"));
list4.AddLast(new Person("Matthias", "Nagel"));
list4.AddLast(new Person("Katharina", "Nagel"));
Console.WriteLine(list4.First);
Console.WriteLine();

#pragma warning disable CA1050 // Declare types in namespaces
public record Person(string FirstName, string LastName);
#pragma warning restore CA1050 // Declare types in namespaces
