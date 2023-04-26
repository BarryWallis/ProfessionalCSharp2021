// See https://aka.ms/new-console-template for more information

using SortingSample;

string[] names =
{
    "Lady Gaga",
    "Shakira",
    "Beyonce",
    "Ava Max",
};
Array.Sort(names);
foreach (string name in names)
{
    Console.WriteLine(name);
}
Console.WriteLine();

Person[] persons =
{
    new("Damon", "Hill"),
    new("Niki", "Lauda"),
    new("Ayrton", "Senna"),
    new("Graham", "Hill"),
};
Array.Sort(persons);
foreach (Person person in persons)
{
    Console.WriteLine(person);
}
Console.WriteLine();

Array.Sort(persons, new PersonComparer(PersonCompareType.FirstName));
foreach (Person person in persons)
{
    Console.WriteLine(person);
}
Console.WriteLine();
