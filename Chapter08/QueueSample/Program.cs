// See https://aka.ms/new-console-template for more information

using QueueSample;

DocumentManager documentManager = new();
Task processDocuments = ProcessDocuments.StartAsync(documentManager);

Random random = new();
for (int i = 0; i < 1000; i++)
{
    Document document = new($"Doc {i}", "content");
    int queueSize = documentManager.AddDocument(document);
    Console.WriteLine($"Added document {document.Title}, queue size: {queueSize}");
    await Task.Delay(random.Next(20));
}


Console.WriteLine($"finished adding documents");
await processDocuments;
Console.WriteLine("bye!");
