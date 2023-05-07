// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;

string uri = (args.Length>=1) ? args[0] : string.Empty;
if (string.IsNullOrEmpty(uri))
{
    Console.Write("Enter an URL (e.g., https://csharp.christiannagel.com): ");
    uri = Console.ReadLine() ?? throw new InvalidOperationException();
}

using HttpClient httpClient = new();
try
{
    using HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(new Uri(uri));
    if (httpResponseMessage.IsSuccessStatusCode)
    {
        string html = await httpResponseMessage.Content.ReadAsStringAsync();
        Console.WriteLine(html[..200]);
    }
    else
    {
        Console.WriteLine($"Status code {httpResponseMessage.StatusCode}: {httpResponseMessage.StatusCode}");
    }
}
catch (UriFormatException ex)
{
    Console.WriteLine($"Error parsing the URI: {ex.Message}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP request exception: {ex.Message}");
}
catch (TaskCanceledException ex)
{
    Console.WriteLine($"Task canceled: {ex.Message}");
}
