using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class Geo
{
    private readonly HttpClient _httpClient;

    internal Geo(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GeoResponse> ReadAsync(string apiSecret)
    {
        const string url = Urls.SAPI_BASE_URL + "/api/geo/account";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiSecret);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        request.Headers.TryAddWithoutValidation("Accept", "application/json");

        return await _httpClient.GetResponseAsync<GeoResponse>(request);
    }

    public async Task<ClusterResponse> UpdateAsync(string apiSecret, IEnumerable<string> allowedCountries, IEnumerable<string> deniedCountries)
    {
        const string url = Urls.SAPI_BASE_URL + "/api/cluster";

        var body = new
        {
            updateAllowedCountries = allowedCountries,
            updateDeniedCountries = deniedCountries
        };

        string content = JsonConvert.SerializeObject(body);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<ClusterResponse>(request);
    }
}
