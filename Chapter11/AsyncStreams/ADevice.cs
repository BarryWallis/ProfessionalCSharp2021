using System.Runtime.CompilerServices;

namespace AsyncStreams;
public record SensorData(int value1, int value2);

    public class ADevice
    {
        private readonly Random _random = new();
        
        public async IAsyncEnumerable<SensorData> GetSensorData([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            while (true)
            {
                await Task.Delay(250, cancellationToken);
                yield return new(_random.Next(20), _random.Next(20));
            }
        }
    }
