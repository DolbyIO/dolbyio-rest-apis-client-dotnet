using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class Geo
{
    private const string URL_BASE = Urls.SAPI_BASE_URL + "/api/geo/account";

    private readonly HttpClient _httpClient;

    internal Geo(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GeoResponse> ReadAsync(string apiSecret)
    {
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, URL_BASE, apiSecret);
        return await _httpClient.GetResponseAsync<GeoResponse>(request);
    }

    public async Task<ClusterResponse> UpdateAsync(string apiSecret, IEnumerable<string> allowedCountries, IEnumerable<string> deniedCountries)
    {
        var body = new
        {
            updateAllowedCountries = allowedCountries,
            updateDeniedCountries = deniedCountries
        };

        string content = JsonConvert.SerializeObject(body);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, URL_BASE, apiSecret, content);
        return await _httpClient.GetResponseAsync<ClusterResponse>(request);
    }
}
