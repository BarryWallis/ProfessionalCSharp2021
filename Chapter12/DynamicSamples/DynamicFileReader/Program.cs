// See https://aka.ms/new-console-template for more information

using DynamicFileReader;

IEnumerable<dynamic> employeeList = DynamicFileHelper.ParseFile("EmployeeList.txt");
foreach (dynamic employee in employeeList)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} lives in {employee.City} {employee.State}.");
}
Console.WriteLine();
