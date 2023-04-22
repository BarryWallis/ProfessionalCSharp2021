// See https://aka.ms/new-console-template for more information

using DefaultInterfaceMethods;

ILogger logger = new ConsoleLogger();
logger.Log("message");
logger.Log(new Exception("sample exception"));
Console.WriteLine();

IEnumerableEx<string> names = new MyCollection<string>()
{
    "James", "Jack", "Jochen", "Sebastian", "Lewis", "Juan",
};

IEnumerable<string> jNames = names.Where(n => n.StartsWith("J"));
foreach (string name in jNames)
{
    Console.WriteLine(name);
}
