using System.Diagnostics;

namespace QueueSample;
public class ProcessDocuments
{
    private readonly DocumentManager _manager;

    protected ProcessDocuments(DocumentManager manager) => _manager = manager;

    public static Task StartAsync(DocumentManager manager) => Task.Run(new ProcessDocuments(manager).RunAsync);

    protected async Task RunAsync()
    {
        Random rnd = new();
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool stop = false;
        do
        {
            if (stopwatch.Elapsed >= TimeSpan.FromSeconds(5))
            {
                stop = true;
            }

            if (_manager.IsDocumentAvailable)
            {
                stopwatch.Restart();
                Document document = _manager.GetDocument();
                Console.WriteLine($"Processing document {document.Title}");
            }

            await Task.Delay(rnd.Next(20));
        } while (!stop);

        Console.WriteLine("stopped reading documents");
    }
}
