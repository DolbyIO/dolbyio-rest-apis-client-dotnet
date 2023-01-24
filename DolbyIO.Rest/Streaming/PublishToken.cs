using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;

namespace DolbyIO.Rest.Streaming;

public sealed class PublishToken
{
    private readonly HttpClient _httpClient;

    internal PublishToken(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReadPublishTokenResponse> ReadAsync(string apiSecret, string tokenId)
    {
        string url = $"{Urls.SAPI_BASE_URL}/api/geo/account/{tokenId}";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiSecret);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        request.Headers.TryAddWithoutValidation("Accept", "application/json");

        return await _httpClient.GetResponseAsync<ReadPublishTokenResponse>(request);
    }

    public async Task<DeletePublishTokenResponse> DeleteAsync(string apiSecret, string tokenId)
    {
        string url = $"{Urls.SAPI_BASE_URL}/api/geo/account/{tokenId}";

        using var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiSecret);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        request.Headers.TryAddWithoutValidation("Accept", "application/json");

        return await _httpClient.GetResponseAsync<DeletePublishTokenResponse>(request);
    }
}
