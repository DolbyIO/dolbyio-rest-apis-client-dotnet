using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace DolbyIO.Rest.Streaming;

public sealed class Whep
{
    private readonly HttpClient _httpClient;

    internal Whep(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets a WHEP endpoint for subscribers.
    /// </summary>
    /// <param name="publishingToken">The publishing token.</param>
    /// <param name="streamAccountId">The account identifier.</param>
    /// <param name="streamName">The name of the stream.</param>
    /// <param name="sourceId">Source ID.</param>
    /// <param name="sdpOffer">SDP offer.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the SDP answer.</returns>
    /// <remarks>This specification is based on the https://www.ietf.org/archive/id/draft-murillo-whep-00.html draft and will be updated to upcoming versions as soon as they are available.</remarks>
    public async Task<string> WhepAsync(string publishingToken, string streamAccountId, string streamName, string sourceId, string sdpOffer)
    {
        var uriBuilder = new UriBuilder(Urls.SAPI_DIRECTOR_BASE_URL)
        {
            Path = $"/api/whep/{streamAccountId}/{streamName}"
        };

        var nvc = new NameValueCollection
        {
            { "sourceId", sourceId }
        };
        uriBuilder.Query = nvc.ToString();

        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, uriBuilder.ToString(),
            publishingToken, sdpOffer, "application/sdp", "application/sdp");
        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        return await response.Content.ReadAsStringAsync();
    }
}
