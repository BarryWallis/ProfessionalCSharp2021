// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;

await UsingTheUnboundedChannelAsync();
await UsingTheBoundedChannelAsync();


async Task UsingTheUnboundedChannelAsync()
{
    Channel<SomeData> channel = Channel.CreateUnbounded<SomeData>(new UnboundedChannelOptions()
    {
        SingleReader = false,
        SingleWriter = true
    });
    Console.WriteLine("Using the unbounded channel");
    Task task1 = ChannelSample.ChannelSample.WriteSomeDataAsync(channel.Writer);
    Task task2 = ChannelSample.ChannelSample.ReadSomeDataAsync(channel.Reader);
    await Task.WhenAll(task1, task2);
    Console.WriteLine();
}

async Task UsingTheBoundedChannelAsync()
{
    Channel<SomeData> channel = Channel.CreateBounded<SomeData>(new BoundedChannelOptions(capacity: 10) { FullMode = BoundedChannelFullMode.Wait, SingleWriter = true });

    Console.WriteLine("Using the bounded channel");

    Task task1 = ChannelSample.ChannelSample.WriteSomeDataWithTryWriteAsync(channel.Writer);
    Task task2 = ChannelSample.ChannelSample.ReadSomeDataUsingAsyncStreams(channel.Reader);

    await Task.WhenAll(task1, task2);

    Console.WriteLine("bye");
    Console.WriteLine();
}
