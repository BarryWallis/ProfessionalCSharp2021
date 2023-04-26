// See https://aka.ms/new-console-template for more information


using OperatorsSample;

PrefixAndPostfix();

int x = 1;
string s = x + " ";
s += x == 1 ? "man" : "men";
Console.WriteLine(s);

byte b = 255;
checked
{
    //b++;
}
Console.WriteLine(b);

Console.WriteLine(sizeof(int));

unsafe
{
    Console.WriteLine(sizeof(Point));
}

static void PrefixAndPostfix()
{
    int x = 5;
    if (++x == 6)
    {
        Console.WriteLine("This will execute");
    }

    if (x++ == 6)
    {
        Console.WriteLine($"The value of x is: {x}");
    }
}
