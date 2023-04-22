// See https://aka.ms/new-console-template for more information
using GenericTypesWithConstraints;

LinkedList<Person> list4 = new();
list4.AddLast(new Person("Stephanie", "Nagel", "Person 1"));
list4.AddLast(new Person("Matthias", "Nagel", "Person 2"));
list4.AddLast(new Person("Katharina", "Nagel", "Person 3"));
list4.DisplayAllTitles();

#pragma warning disable CA1050 // Declare types in namespaces
public record Person(string FirstName, string LastName, string Title) : ITitle
{

}
#pragma warning restore CA1050 // Declare types in namespaces
