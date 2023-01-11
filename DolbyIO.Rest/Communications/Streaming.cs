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
    /// Starts the RTMP live stream for the specified conference.
    /// Once the Dolby.io Communication API service started streaming to the target url,
    /// a <c>Stream.Rtmp.InProgress</c> Webhook event will be sent.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/start-rtmp"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="rtmpUrl">RTMP endpoint where to send the RTMP stream to.</param>
    /// <param name="layoutUrl">
    /// Overwrites the layout URL configuration:
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>null</c></term>
    ///         <description>uses the layout URL configured in the dashboard (if no URL is set in the dashboard, then uses the Dolby.io default).</description>
    ///     </item>
    ///     <item>
    ///         <term><c>default</c></term>
    ///         <description>uses the Dolby.io default layout.</description>
    ///     </item>
    ///     <item>
    ///         <term>URL string</term>
    ///         <description>uses this layout URL.</description>
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="layoutName">Defines a name for the given layout URL, which makes layout identification easier for customers especially when the layout URL is not explicit.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StartRtmpAsync(JwtToken accessToken, string conferenceId, string rtmpUrl, string layoutUrl = null, string layoutName = null)
    {
        var requestObject = new StartRtmpRequest
        {
            Uri = rtmpUrl,
            LayoutUrl = layoutUrl,
            LayoutName = layoutName
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rtmp/start";
        await _httpClient.SendPostAsync(url, accessToken, requestObject);
    }

    /// <summary>
    /// Stops the RTMP stream of the specified conference.<br/>
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
    /// Starts real-time streaming using Dolby.io Real-time Streaming services (formerly Millicast).<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/start-rts"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="streamName"></param>
    /// <param name="publishingToken"></param>
    /// <param name="layoutUrl">
    /// Overwrites the layout URL configuration:
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>null</c></term>
    ///         <description>uses the layout URL configured in the dashboard (if no URL is set in the dashboard, then uses the Dolby.io default).</description>
    ///     </item>
    ///     <item>
    ///         <term><c>default</c></term>
    ///         <description>uses the Dolby.io default layout.</description>
    ///     </item>
    ///     <item>
    ///         <term>URL string</term>
    ///         <description>uses this layout URL.</description>
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="layoutName">Defines a name for the given layout URL, which makes layout identification easier for customers especially when the layout URL is not explicit.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task StartRtsAsync(JwtToken accessToken, string conferenceId, string streamName, string publishingToken, string layoutUrl = null, string layoutName = null)
    {
        var requestObject = new StartRtsRequest
        {
            StreamName = streamName,
            PublishingToken = publishingToken,
            LayoutUrl = layoutUrl,
            LayoutName = layoutName
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/rts/start";
        await _httpClient.SendPostAsync(url, accessToken, requestObject);
    }

    /// <summary>
    /// Stops real-time streaming to Dolby.io Real-time Streaming services.<br/>
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
