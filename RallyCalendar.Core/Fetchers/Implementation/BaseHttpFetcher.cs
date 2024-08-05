using System.Text.Json;

namespace RallyCalendar.Core.Fetchers.Implementation;
public abstract class BaseHttpFetcher
{
    private readonly HttpClient _httpClient;

    public BaseHttpFetcher()
    {
        _httpClient = new HttpClient();
    }

    protected async Task<TResponse> GetAsync<TResponse>(string endpoint)
    {
        var httpResponse = await _httpClient.GetAsync(endpoint);
        httpResponse.EnsureSuccessStatusCode();

        var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(httpResponseBody))
            throw new Exception("The response body was null or empty.");

        var result = JsonSerializer.Deserialize<TResponse>(httpResponseBody);

        if (result == null)
            throw new Exception("Failed to deserialize the response.");

        return result;
    }

    protected void SetBaseAddress(string endpoint) 
    {
        _httpClient.BaseAddress = new Uri(endpoint);
    }
}
