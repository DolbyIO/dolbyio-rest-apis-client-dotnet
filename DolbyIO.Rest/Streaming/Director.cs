using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class Director
{
    private readonly HttpClient _httpClient;

    internal Director(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Requests for url and authorization to publish a stream.
    /// </summary>
    /// <param name="apiSecret">Your Dolby.io API Secret.</param>
    /// <param name="streamName">The name of the stream.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="PublishResponse" /> object.</returns>
    public async Task<PublishResponse> PublishAsync(string apiSecret, string streamName)
    {
        const string url = Urls.SAPI_DIRECTOR_BASE_URL + "/api/director/publish";

        var body = new
        {
            streamName
        };

        string content = JsonConvert.SerializeObject(body);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<PublishResponse>(request);
    }

    /// <summary>
    /// Requests for url and authorization to subscribe to a stream.
    /// </summary>
    /// <param name="apiSecret">Your Dolby.io API Secret.</param>
    /// <param name="streamName">The name of the stream.</param>
    /// <param name="streamAccountId">The account identifier. Required only for published streams which have subscribeRequiresAuth=false.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="SubscribeResponse" /> object.</returns>
    public async Task<SubscribeResponse> SubscribeAsync(string apiSecret, string streamName, string streamAccountId)
    {
        const string url = Urls.SAPI_DIRECTOR_BASE_URL + "/api/director/subscribe";

        var body = new
        {
            streamName,
            streamAccountId
        };

        string content = JsonConvert.SerializeObject(body);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<SubscribeResponse>(request);
    }
}
