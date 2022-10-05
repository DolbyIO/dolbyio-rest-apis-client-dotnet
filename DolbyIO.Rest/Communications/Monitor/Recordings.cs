using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Communications.Monitor;

public sealed class Recordings
{
    private readonly HttpClient _httpClient;

    internal Recordings(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Get a list of the recorded conference metadata, such as duration or size of the recording.
    /// This API checks only the recordings that have ended during a specific time range.
    /// Recordings are indexed based on the ending time.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-recordings"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the recordings.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="GetRecordingsResponse" /> object.</returns>
    public async Task<GetRecordingsResponse> GetRecordingsAsync(JwtToken accessToken, GetRecordingsOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = "/v1/monitor/recordings";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.Max ?? 100).ToString());
        if (!string.IsNullOrWhiteSpace(options.Start))
            nvc.Add("start", options.Start);

        uriBuilder.Query = nvc.ToString();

        return await _httpClient.SendPostAsync<GetRecordingsResponse>(uriBuilder.Uri.ToString(), accessToken);
    }

    /// <summary>
    /// Get a list of the recorded conference metadata, such as duration or size of the recording.
    /// This API checks only the recordings that have ended during a specific time range.
    /// Recordings are indexed based on the ending time.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-recordings"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the recordings.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of <see cref="Models.Recording" /> objects.</returns>
    public async Task<IEnumerable<Models.Recording>> GetAllRecordingsAsync(JwtToken accessToken, GetAllRecordingsOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = "/v1/monitor/recordings";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.PageSize ?? 100).ToString());

        uriBuilder.Query = nvc.ToString();

        List<Models.Recording> result = new List<Models.Recording>();

        GetRecordingsResponse response;
        do
        {
            response = await _httpClient.SendPostAsync<GetRecordingsResponse>(uriBuilder.Uri.ToString(), accessToken);
            result.AddRange(response.Recordings);

            nvc.Add("start", response.Next);
            uriBuilder.Query = nvc.ToString();
        } while (!string.IsNullOrWhiteSpace(response.Next));

        return result;
    }

    /// <summary>
    /// Get a list of the recorded conference metadata, such as duration or size of the recording.
    /// This API checks the recordings that have ended during a specific time range.
    /// Recordings are indexed based on the ending time.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-conference-recordings"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the recording.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of <see cref="Models.Recording" /> objects.</returns>
    public async Task<IEnumerable<Models.Recording>> GetRecordingAsync(JwtToken accessToken, GetRecordingOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = $"/v1/monitor/conferences/{options.ConferenceId}/recordings";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.Max ?? 100).ToString());
        if (!string.IsNullOrWhiteSpace(options.Start))
            nvc.Add("start", options.Start);

        uriBuilder.Query = nvc.ToString();

        GetRecordingsResponse response = await _httpClient.SendPostAsync<GetRecordingsResponse>(uriBuilder.Uri.ToString(), accessToken);
        return response.Recordings;
    }

    /// <summary>
    /// Delete all recording data related to a specific conference.<br />
    /// Warning: After deleting the recording, it is not possible to restore the recording data.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/delete-conference-recordings"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteRecordingAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v1/monitor/conferences/{conferenceId}/recordings";
        await _httpClient.SendDeleteAsync(url, accessToken);
    }

    /// <summary>
    /// Get details of all Dolby Voice-based audio recordings, and associated split recordings,
    /// for a given conference and download the conference recording in the MP3 audio format.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-dolby-voice-audio-recordings"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="DolbyVoiceRecording" /> object.</returns>
    public async Task<DolbyVoiceRecording> GetDolbyVoiceRecordingAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v1/monitor/conferences/{conferenceId}/recordings/audio";
        return await _httpClient.SendGetAsync<DolbyVoiceRecording>(url, accessToken);
    }
}
