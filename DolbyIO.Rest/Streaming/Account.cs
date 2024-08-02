using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class Account
{
    private readonly HttpClient _httpClient;

    internal Account(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GeoCascade> ReadGeoCascadeAsync(string apiSecret)
    {
        const string url = Urls.STREAMING_BASE_URL + "/api/account/geo_cascade";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<GeoCascade>(request);
    }

    public async Task<GeoCascade> UpdateGeoCascadeAsync(string apiSecret, UpdateGeoCascade rules)
    {
        const string url = Urls.STREAMING_BASE_URL + "/api/account/geo_cascade";
        string content = JsonConvert.SerializeObject(rules);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Put, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<GeoCascade>(request);
    }

    public async Task<GeoRestrictions> ReadGeoRestrictionsAsync(string apiSecret)
    {
        const string url = Urls.STREAMING_BASE_URL + "/api/geo/account";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<GeoRestrictions>(request);
    }

    public async Task<GeoRestrictions> UpdateGeoRestrictionsAsync(string apiSecret, IEnumerable<string> allowedCountries, IEnumerable<string> deniedCountries)
    {
        const string url = Urls.STREAMING_BASE_URL + "/api/geo/account";
        var body = new UpdateGeoRestrictions
        {
            UpdateAllowedCountries = allowedCountries,
            UpdateDeniedCountries = deniedCountries
        };

        string content = JsonConvert.SerializeObject(body);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<GeoRestrictions>(request);
    }
}
