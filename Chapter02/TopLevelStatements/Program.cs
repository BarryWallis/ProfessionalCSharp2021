// See https://aka.ms/new-console-template for more information

#pragma warning disable IDE0090 // Use 'new(...)'
string s1 = new string("Hello, World!");
#pragma warning restore IDE0090 // Use 'new(...)'
string s2 = "Hello, World!";
#pragma warning disable IDE0008 // Use explicit type
var s3 = "Hello, World!";
#pragma warning restore IDE0008 // Use explicit type
string s4 = new("Hello, World!");

Console.WriteLine(s1);
Console.WriteLine(s2);
Console.WriteLine(s3);
Console.WriteLine(s4);

#pragma warning disable IDE0062 // Make local function 'static'
void Method()
{
#pragma warning disable IDE0061 // Use expression body for local function
    Console.WriteLine("this is a method");
#pragma warning restore IDE0061 // Use expression body for local function
}
#pragma warning restore IDE0062 // Make local function 'static'

Method();

