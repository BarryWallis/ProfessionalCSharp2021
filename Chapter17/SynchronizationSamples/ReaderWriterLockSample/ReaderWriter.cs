using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterLockSample;

public sealed class ReaderWriter : IDisposable
{
    private readonly List<int> _items = Enumerable.Range(0, 6).ToList();
    private readonly ReaderWriterLockSlim _lockSlim = new();
    private bool _disposedValue;

    public void ReaderMethod(object? reader)
    {
        try
        {
            _lockSlim.EnterReadLock();

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"reader {reader}, loop: {i}, item: {_items[i]}");
                Task.Delay(40).Wait();
            }
        }
        finally
        {
            _lockSlim.ExitReadLock();
        }
    }

    public void WriterMethod(object? writer)
    {
        try
        {
            while (!_lockSlim.TryEnterWriteLock(50))
            {
                Console.WriteLine($"Writer {writer} waiting for write lock");
                Console.WriteLine($"current reader count: {_lockSlim.CurrentReadCount}");
            }

            Console.WriteLine($"Writer {writer} acquired the lock");
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i] += 1;
                Task.Delay(50).Wait();
            }
            Console.WriteLine($"Writer {writer} finished");
        }
        finally
        {
            _lockSlim.ExitWriteLock();
        }
    }

    public void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _lockSlim?.Dispose();
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
