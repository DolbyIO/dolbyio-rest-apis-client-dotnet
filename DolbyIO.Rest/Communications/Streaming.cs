using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Communications;

public sealed class Streaming
{
    private readonly HttpClient _httpClient;

    internal Streaming(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts an RTMP live stream. Once the Dolby.io Communication API service started streaming to the target url,
    /// a <c>Stream.Rtmp.InProgress</c> Webhook event will be sent.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/start-rtmp"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="rtmpUrl">RTMP endpoint where to send the RTMP stream to.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StartRtmpAsync(JwtToken accessToken, string conferenceId, string rtmpUrl)
    {
        var requestObject = new StartRtmpRequest { Uri = rtmpUrl };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rtmp/start";
        await _httpClient.SendPostAsync(url, accessToken, requestObject);
    }

    /// <summary>
    /// Starts an RTMP live stream. Once the Dolby.io Communication API service started streaming to the target url,
    /// a <c>Stream.Rtmp.InProgress</c> Webhook event will be sent.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/start-rtmp"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="rtmpUrls">List of the RTMP endpoints where to send the RTMP stream to.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StartRtmpAsync(JwtToken accessToken, string conferenceId, IEnumerable<string> rtmpUrls)
    {
        var requestObject = new StartRtmpRequest { Uri = string.Join("|", rtmpUrls) };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rtmp/start";
        await _httpClient.SendPostAsync(url, accessToken, requestObject);
    }

    /// <summary>
    /// Stops an RTMP stream.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/stop-rtmp"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StopRtmpAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rtmp/stop";
        await _httpClient.SendPostAsync(url, accessToken);
    }

    /// <summary>
    /// <br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/start-rts"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="streamName"></param>
    /// <param name="publishingToken"></param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StartRtsAsync(JwtToken accessToken, string conferenceId, string streamName, string publishingToken)
    {
        var requestObject = new StartRtsRequest
        {
            StreamName = streamName,
            PublishingToken = publishingToken
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rts/start";
        await _httpClient.SendPostAsync(url, accessToken, requestObject);
    }

    /// <summary>
    /// <br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/stop-rts"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StopRtsAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rts/stop";
        await _httpClient.SendPostAsync(url, accessToken);
    }
}
