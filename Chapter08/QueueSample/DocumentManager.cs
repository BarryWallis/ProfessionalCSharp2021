namespace QueueSample;
public class DocumentManager
{
    private readonly object _syncQueue = new();
    private readonly Queue<Document> _documentQueue = new();

    public int AddDocument(Document document)
    {
        lock (_syncQueue)
        {
            _documentQueue.Enqueue(document);
            return _documentQueue.Count;
        }
    }

    public Document GetDocument()
    {
        Document document;
        lock (_syncQueue)
        {
            document = _documentQueue.Dequeue();
        }

        return document;
    }

    public bool IsDocumentAvailable => _documentQueue.Count > 0;
}
