using Microsoft.Extensions.Logging;

namespace LoggingSample;
public class NetworkService
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public NetworkService(HttpClient httpClient, ILogger<NetworkService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _logger.LogTrace($"Ilogger injected into {nameof(NetworkService)}");
    }

    public async Task NetworkRequestSampleAsync(Uri requestUri)
    {
        try
        {
            _logger.LogInformation(
                LoggingEvents.Networking,
                "NetworkRequestSampleAsync started with url {RequestUri}", requestUri.AbsoluteUri);
            string result = await _httpClient.GetStringAsync(requestUri);
            Console.WriteLine($"{result[..50]}");
            _logger.LogInformation(
                LoggingEvents.Networking,
                "NetworkRequestSampleAsync completed, received {length} characters", result.Length);
        }
        //catch (HttpRequestException ex)
        catch (Exception ex)
        {
            _logger.LogError(LoggingEvents.Networking, ex, "Error in NetworkRequestSampleAsync, error message {Message}, HResult: {HResult}", ex.Message, ex.HResult);
        }
    }
}

public class Runner
{
    private readonly ILogger _logger;
    private readonly NetworkService _networkService;

    public Runner(NetworkService networkService, ILogger logger)
    {
        _networkService = networkService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogDebug($"{nameof(RunAsync)} started");
        bool exit = false;
        do
        {
            Console.Write("Please enter a URI or [ENTER] to exit: ");
            string? url = Console.ReadLine();
            if (string.IsNullOrEmpty(url))
            {
                exit = true;
            }
            else
            {
                try
                {
                    Uri uri = new(url);
                    await _networkService.NetworkRequestSampleAsync(uri);
                }
                catch (UriFormatException ex)
                {
#pragma warning disable CA2254 // Template should be a static expression
                    _logger.LogError(ex, ex.Message);
#pragma warning restore CA2254 // Template should be a static expression
                }
            }
        } while (!exit);
    }
}
