// See https://aka.ms/new-console-template for more information

LinkedList<Document> documents = new();
LinkedListNode<Document> first = documents.AddFirst(new Document(1, "first"));
documents.AddAfter(first, new Document(2, "after first"));
LinkedListNode<Document> last = documents.AddLast(new Document(3, "Last"));
Document document4 = new(4, "before last");
documents.AddBefore(last, document4);

foreach (Document document in documents)
{
    Console.WriteLine(document);
}
Console.WriteLine();

if (documents.First is not null)
{
    IterateUsingNext(documents.First);
}
Console.WriteLine();

documents.Remove(document4);
foreach (Document document in documents)
{
    Console.WriteLine(document);
}
Console.WriteLine();

static void IterateUsingNext(LinkedListNode<Document> start)
{
    if (start.Value is null)
    {
        return;
    }

    LinkedListNode<Document>? current = start;
    do
    {
        Console.WriteLine(current.Value);
        current = current.Next;
    } while (current is not null);
}

#pragma warning disable CA1050 // Declare types in namespaces
public record Document(int Id, string Text);
#pragma warning restore CA1050 // Declare types in namespaces
