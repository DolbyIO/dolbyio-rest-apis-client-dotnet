using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class Cluster
{
    private readonly HttpClient _httpClient;

    internal Cluster(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ClusterResponse> ReadAsync(string apiSecret)
    {
        const string url = Urls.SAPI_BASE_URL + "/api/cluster";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiSecret);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        request.Headers.TryAddWithoutValidation("Accept", "application/json");

        return await _httpClient.GetResponseAsync<ClusterResponse>(request);
    }

    public async Task<ClusterResponse> UpdateAsync(string apiSecret, string defaultCluster)
    {
        const string url = Urls.SAPI_BASE_URL + "/api/cluster";

        var body = new
        {
            defaultCluster = defaultCluster
        };

        string content = JsonConvert.SerializeObject(body);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Put, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<ClusterResponse>(request);
    }
}
