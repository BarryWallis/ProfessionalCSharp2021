// See https://aka.ms/new-console-template for more information

using UsingInterfaces;

Ellipse e1 = new(new ConsoleLogger())
{
    Position = new(20, 30),
    Size = new(100, 120)
};
e1.Draw();
