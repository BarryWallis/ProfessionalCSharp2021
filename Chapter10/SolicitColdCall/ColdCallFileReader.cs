// See https://aka.ms/new-console-template for more information

namespace SolicitColdCall;

public class ColdCallFileReader : IDisposable
{
    private FileStream? _fileStream;
    private StreamReader? _streamReader;
    private uint _numberOfPeopleToRing;
    private bool _isDisposed = false;
    private bool _isOpen = false;

    public uint NumberOfPeopleToRing
        => _isDisposed
           ? throw new ObjectDisposedException("peopleToRing")
           : !_isOpen
           ? throw new UnexpectedException("Attempted to access cold-call file that is not open")
           : _numberOfPeopleToRing;

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        GC.SuppressFinalize(this);
        _isDisposed = true;
        _isOpen = false;

        _streamReader?.Dispose();
        _streamReader = null;
    }

    public void Open(string fileName)
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ColdCallFileReader));
        }

        _fileStream = new(fileName, FileMode.Open);
        _streamReader = new(_fileStream);

        try
        {
            string? firstLine = _streamReader.ReadLine();
            if (firstLine is not null)
            {
                _numberOfPeopleToRing = uint.Parse(firstLine);
                _isOpen = true;
            }
        }
        catch (FormatException ex)
        {
            throw new ColdCallFileFormatException($"First line isn't an integer {ex}");
        }
    }

    internal void ProcessNextPerson()
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ColdCallFileReader));
        }

        if (!_isOpen)
        {
            throw new UnexpectedException("Attempted to access cold-call file that is not open");
        }

        try
        {
            string? name = (_streamReader?.ReadLine())
                           ?? throw new ColdCallFileFormatException("Not enough names");
            if (name[0] is 'B')
            {
                throw new SalesSpyFoundException(name);
            }

            Console.WriteLine(name);
        }
        catch (SalesSpyFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // This section intentionally left blank
        }
    }
}
