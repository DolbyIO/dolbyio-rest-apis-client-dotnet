using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class Stream
{
    private readonly HttpClient _httpClient;

    internal Stream(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Stop an active stream by its Stream ID.
    /// </summary>
    /// <param name="apiSecret">API Secret.</param>
    /// <param name="streamId">ID of the stream to stop.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    /// <remarks>Prior to stopping all streams, you must call the <xref href="DolbyIO.Rest.Streaming.PublishToken.DisableAsync"/> function.</remarks>
    public async Task StopAsync(string apiSecret, string streamId)
    {
        const string url = Urls.STREAMING_BASE_URL + "/api/stream/stop";

        var body = new
        {
            streamId = streamId
        };
        string content = JsonConvert.SerializeObject(body);

        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, url, apiSecret, content);
        await _httpClient.SendRequestAsync(request);
    }

    /// <summary>
    /// Stop all currently active streams associated with your account.
    /// </summary>
    /// <param name="apiSecret">API Secret.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    /// <remarks>Prior to stopping all streams, you must call the <xref href="DolbyIO.Rest.Streaming.PublishToken.DisableAsync"/> function.</remarks>
    public async Task StopAllAsync(string apiSecret)
    {
        const string url = Urls.STREAMING_BASE_URL + "/api/stream/stop/all";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Post, url, apiSecret);
        await _httpClient.SendRequestAsync(request);
    }
}

