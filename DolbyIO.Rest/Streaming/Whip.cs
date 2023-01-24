using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace DolbyIO.Rest.Streaming;

public sealed class Whip
{
    private readonly HttpClient _httpClient;

    internal Whip(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets a WHIP endpoint for publishers.
    /// </summary>
    /// <param name="apiSecret">Your Dolby.io API Secret.</param>
    /// <param name="streamName">The name of the stream.</param>
    /// <param name="codec">Codec.</param>
    /// <param name="sourceId">Source ID.</param>
    /// <param name="sdpOffer">SDP offer.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the SDP answer.</returns>
    public async Task<string> WhipAsync(string apiSecret, string streamName, string codec, string sourceId, string sdpOffer)
    {
        var uriBuilder = new UriBuilder(Urls.SAPI_DIRECTOR_BASE_URL)
        {
            Path = $"/api/whip/{streamName}"
        };

        var nvc = new NameValueCollection
        {
            { "codec", codec },
            { "sourceId", sourceId }
        };
        uriBuilder.Query = nvc.ToString();

        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, uriBuilder.ToString(),
            apiSecret, sdpOffer, "application/sdp", "application/sdp");
        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        return await response.Content.ReadAsStringAsync();
    }
}
