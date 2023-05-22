using Microsoft.Extensions.Logging;

namespace MetricsSample;

public class Runner
{
    private readonly ILogger _logger;
    private readonly NetworkService _networkService;
    public Runner(NetworkService networkService, ILogger<Runner> logger)
    {
        _networkService = networkService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogDebug("RunAsync started");
        bool exit = false;
        do
        {
            Console.Write("Please enter a URI or 'exit' to exit: ");
            string url = Console.ReadLine() ?? throw new InvalidOperationException("null returned from Console.ReadLine");
            using IDisposable? _ = _logger.BeginScope("RunAsync iteration, url: {url}", url);
            if (url.ToLower() != "exit")
            {
                try
                {
                    Uri uri = new(url);
                    await _networkService.NetworkRequestSampleAsync(uri);
                }
                catch (UriFormatException ex)
                {
                    _logger.LogError(ex, "Error {message}", ex.Message);
                }
            }
            else
            {
                exit = true;
            }
        } while (!exit);
    }
}
