using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockAcrossAwait;

public sealed class AsyncSemaphore
{
    private class SemaphoreReleaser : IDisposable
    {
        private readonly SemaphoreSlim _semaphore;

        public SemaphoreReleaser(SemaphoreSlim semaphore) => _semaphore = semaphore;

        public void Dispose() => _semaphore.Release();
    }

    private readonly SemaphoreSlim _semaphore;

    public AsyncSemaphore() => _semaphore = new(1);

    public async Task<IDisposable> WaitAsync()
    {
        await _semaphore.WaitAsync();
        return new SemaphoreReleaser(_semaphore);
    }
 }
