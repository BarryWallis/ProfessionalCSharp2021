// See https://aka.ms/new-console-template for more information

Stack<char> alphabet = new();
alphabet.Push('A');
alphabet.Push('B');
alphabet.Push('C');
Console.Write("First iteration: ");
foreach (char c in alphabet)
{
    Console.Write(c);
}
Console.WriteLine();
Console.Write("Second iteration: ");
while (alphabet.Count > 0)
{
    Console.Write(alphabet.Pop());
}
Console.WriteLine();
