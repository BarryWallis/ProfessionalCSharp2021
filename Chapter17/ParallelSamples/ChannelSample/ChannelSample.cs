// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;

namespace ChannelSample;

public static class ChannelSample
{
    public static Task ReadSomeDataAsync(ChannelReader<SomeData> reader) => Task.Run(async () =>
    {
        try
        {
            Console.WriteLine("Start reading...");
            Random random = new();
            while (true)
            {
                await Task.Delay(random.Next(80));
                SomeData someData = await reader.ReadAsync();
                Console.WriteLine($"read: {someData.Text}, available items: {reader.Count}");
            }
        }
        catch (ChannelClosedException)
        {
            Console.WriteLine($"channel closed");
        }
    });

    public static Task ReadSomeDataUsingAsyncStreams(ChannelReader<SomeData> reader) => Task.Run(async () =>
    {
        try
        {
            Console.WriteLine("Start reading...");
            Random random = new();
            await foreach (SomeData someData in reader.ReadAllAsync())
            {
                await Task.Delay(random.Next(80));
                Console.WriteLine($"read: {someData.Text}, available items: {reader.Count}");
            }
        }
        catch (ChannelClosedException)
        {
            Console.WriteLine($"channel closed");
        }
    });

    public static Task WriteSomeDataAsync(ChannelWriter<SomeData> writer) => Task.Run(async () =>
    {
        for (int i = 0; i < 100; i++)
        {
            Random random = new();
            SomeData someData = new($"text {i}", i);
            await Task.Delay(random.Next(50));
            await writer.WriteAsync(someData);
            Console.WriteLine($"Written {someData.Text}");
        }
        writer.Complete();
        Console.WriteLine($"Writing completed");
    });

    public static Task WriteSomeDataWithTryWriteAsync(ChannelWriter<SomeData> writer) => Task.Run(async () =>
    {
        for (int i = 0; i < 100; i++)
        {
            Random random = new();
            SomeData someData = new($"text {i}", i);
            await Task.Delay(random.Next(50));
            if (!writer.TryWrite(someData))
            {
                Console.WriteLine($"could not write {someData.Number}, channel full");
            }
            else
            {
                Console.WriteLine($"Written someData {someData.Text}");
            }
        }
        writer.Complete();
        Console.WriteLine($"Writing completed");
    });
}
