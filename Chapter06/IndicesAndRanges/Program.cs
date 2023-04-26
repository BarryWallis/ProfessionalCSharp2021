// See https://aka.ms/new-console-template for more information

using IndicesAndRanges;

int[] data = Enumerable.Range(1, 9).ToArray();

int first1 = data[0];
int last1 = data[^1];
Console.WriteLine($"first: {first1}, last: {last1}");

int last2 = data[^1];
Console.WriteLine(last2);

Index firstIndex = 0;
Index lastIndex = ^1;
int first3 = data[firstIndex];
int last3 = data[lastIndex];
Console.WriteLine($"first: {first3}, last: {last3}");
Console.WriteLine();

ShowRange("full range", data[..]);
ShowRange("first three", data[0..3]);
ShowRange("fourth to sixth", data[3..6]);
ShowRange("last three", data[^3..^0]);

int[] slice1 = data[3..5];
slice1[0] = 42;
Console.WriteLine($"value in array didn't change: {data[3]}, value from slice: {slice1[0]}");

Span<int> sliceToSpan = data.AsSpan()[3..5];
sliceToSpan[0] = 42;
Console.WriteLine($"value in array: {data[3]}, value from slice: {sliceToSpan[0]}");
Console.WriteLine();

MyCollection myCollection = new();
int n = myCollection[^20];
Console.WriteLine($"Item from the collection: {n}");
ShowRange("Using custom collection", myCollection[45..^40]);

static void ShowRange(string title, int[] data)
{
    Console.WriteLine(title);

    Console.WriteLine(string.Join(" ", data));
    Console.WriteLine();
}
