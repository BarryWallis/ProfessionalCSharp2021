// See https://aka.ms/new-console-template for more information

using CustomIndexerSample;

Person person1 = new("Ayrton", "Senna", new(1960, 3, 21));
Person person2 = new("Ronnie", "Peterson", new(1944, 2, 14));
Person person3 = new("Jochen", "Rindt", new(1942, 4, 18));
Person person4 = new("Francois", "Cevert", new(1944, 2, 25));
PersonCollection collection = new(person1, person2, person3, person4);
Console.WriteLine(collection[2]);
foreach (Person person in collection[new DateTime(1960, 3, 21)])
{
    Console.WriteLine(person);
}
