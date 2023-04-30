// See https://aka.ms/new-console-template for more information

using DictionarySample;

EmployeeId idKyle = new("J18");
Employee kyle = new(idKyle, "Kyle Bush", 138_000.00M);

EmployeeId idMartin = new("J19");
Employee martin = new(idMartin, "Martin Truex", 73_000.00M);

EmployeeId idKevin = new("S4");
Employee kevin = new(idKevin, "Kevin Harvick", 116_000.00M);

EmployeeId idDenny = new("J11");
Employee denny = new(idDenny, "Denny Hamlin", 127_000.00M);

EmployeeId idJoey = new("T22");
Employee joey = new(idKyle, "Joey Logano", 96_000.00M);

EmployeeId idKyleL = new("C42");
#pragma warning disable IDE0059 // Unnecessary assignment of a value
Employee kyleL = new(idKyleL, "Kyle Larson", 80_000.00M);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

Dictionary<EmployeeId, Employee> employees = new(31)
{
    [idKyle] = kyle,
    [idMartin] = martin,
    [idKevin] = kevin,
    [idDenny] = denny,
    [idJoey] = joey,
};

foreach (Employee employee in employees.Values)
{
    Console.WriteLine(employee);
}

while (true)
{
    Console.Write("Enter employee id (X to exit)> ");
    string? userInput = Console.ReadLine();
    userInput = userInput?.ToUpper();
    if (userInput is null or "")
    {
        continue;
    }
    if (userInput == "X")
    {
        break;
    }

    try
    {
        EmployeeId id = new(userInput);
        if (!employees.TryGetValue(id, out Employee? employee))
        {
            Console.WriteLine($"Employee with id {id} does not exist");
        }
        else
        {
            Console.WriteLine(employee);
        }
    }
    catch (EmployeeIdException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
