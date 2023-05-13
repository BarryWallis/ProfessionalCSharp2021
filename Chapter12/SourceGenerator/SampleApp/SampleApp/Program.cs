// See https://aka.ms/new-console-template for more information

using SampleApp;

CodeGenerationSample.HelloWorld.Hello();
Console.WriteLine();

Book b1 = new("Professional C#", "Wrox Press");
Book b2 = new("Professional C#", "Wrox Press");
if  (b1 == b2)
{
    Console.WriteLine("the same book");
}
