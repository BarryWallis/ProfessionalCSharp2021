using AsyncStreams;

CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(5));

ADevice aDevice = new();
try
{
    await foreach (SensorData data in aDevice.GetSensorData()
                                             .WithCancellation(cancellationTokenSource.Token))
    {
        Console.WriteLine($"{data.value1} {data.value2}");
    }
}
catch (OperationCanceledException ex)
{
    Console.WriteLine(ex.Message);
}

return 0;

