﻿internal class Program
{
    private static async Task Main()
    {
        for (int i = 0; i < 20; i++)
        {
            _ = await GetSomeDataAsync();
            await Task.Delay(1000);
        }
    }

    private static DateTime _retrieved;
    private static IEnumerable<string> _cachedData = new List<string>();

    public static Task<(IEnumerable<string> data, DateTime retruevedTime)> GetTheRealData()
        => Task.FromResult((Enumerable.Range(0, 10).Select(x => $"item {x}").AsEnumerable(), DateTime.Now));

    public static async ValueTask<IEnumerable<string>> GetSomeDataAsync()
    {
        if (_retrieved >= DateTime.Now.AddSeconds(-5)) 
        {
            Console.WriteLine("data from the cache");
            return await new ValueTask<IEnumerable<string>>(_cachedData);
        }

        Console.WriteLine($"data from the service");
        (_cachedData, _retrieved) = await GetTheRealData();
        return _cachedData;
    }
}
