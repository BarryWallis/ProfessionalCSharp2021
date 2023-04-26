// See https://aka.ms/new-console-template for more information

string mid = ", middle part,";
Func<string, string> anonymousDelegate = delegate (string param)
{
    param += mid;
    param += " and ths was added to the string.";
    return param;
};
Console.WriteLine(anonymousDelegate("Start of string"));
