using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Communications.Monitor;

public sealed class Conferences
{
    private readonly HttpClient _httpClient;

    internal Conferences(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Get a list of conferences that were started in a specific time range, including ongoing conferences.<br />
    /// Note: Only terminated conferences include a complete summary.<br />
    /// The summary of ongoing conferences includes the following fields in the response:
    /// <c>confId</c>, <c>alias</c>, <c>region</c>, <c>dolbyVoice</c>, <c>start</c>, <c>live</c>, <c>owner</c>.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-conferences"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the conferences.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="ListConferencesResponse" /> object.</returns>
    public async Task<ListConferencesResponse> ListConferencesAsync(JwtToken accessToken, ListConferencesOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = "/v1/monitor/conferences";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.Max ?? 100).ToString());
        nvc.Add("active", options.Active.ToString());
        nvc.Add("livestats", options.LiveStats.ToString());
        if (!string.IsNullOrWhiteSpace(options.Start))
            nvc.Add("start", options.Start);
        if (!string.IsNullOrWhiteSpace(options.Alias))
            nvc.Add("alias", options.Alias);
        if (!string.IsNullOrWhiteSpace(options.ExternalId))
            nvc.Add("exid", options.ExternalId);

        uriBuilder.Query = nvc.ToString();

        return await _httpClient.SendPostAsync<ListConferencesResponse>(uriBuilder.Uri.ToString(), accessToken);
    }

    /// <summary>
    /// Get a list of all the conferences that were started in a specific time range, including ongoing conferences.<br />
    /// Note: Only terminated conferences include a complete summary.<br />
    /// The summary of ongoing conferences includes the following fields in the response:
    /// <c>confId</c>, <c>alias</c>, <c>region</c>, <c>dolbyVoice</c>, <c>start</c>, <c>live</c>, <c>owner</c>.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-conferences"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the conferences.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of <see cref="ConferenceDetails" /> objects.</returns>
    public async Task<IEnumerable<ConferenceInfo>> ListAllConferencesAsync(JwtToken accessToken, ListAllConferencesOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = "/v1/monitor/conferences";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.PageSize ?? 100).ToString());
        nvc.Add("active", options.Active.ToString());
        nvc.Add("livestats", options.LiveStats.ToString());
        if (!string.IsNullOrWhiteSpace(options.Alias))
            nvc.Add("alias", options.Alias);
        if (!string.IsNullOrWhiteSpace(options.ExternalId))
            nvc.Add("exid", options.ExternalId);

        uriBuilder.Query = nvc.ToString();

        List<ConferenceInfo> result = new List<ConferenceInfo>();

        ListConferencesResponse response;
        do
        {
            response = await _httpClient.SendPostAsync<ListConferencesResponse>(uriBuilder.Uri.ToString(), accessToken);
            result.AddRange(response.Conferences);

            nvc.Add("start", response.Next);
            uriBuilder.Query = nvc.ToString();
        } while (!string.IsNullOrWhiteSpace(response.Next));

        return result;
    }

    /// <summary>
    /// Get a summary of a conference.<br />
    /// Note: Only terminated conferences include a complete summary.<br />
    /// The summary of ongoing conferences includes the following fields in the response:
    /// <c>confId</c>, <c>alias</c>, <c>region</c>, <c>dolbyVoice</c>, <c>start</c>, <c>live</c>, <c>owner</c>.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-conference-summary"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">The conference identifier.</param>
    /// <param name="liveStats">For live conferences, the number of `user`, `listener`, and `pstn` participants.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="ConferenceDetails" /> object.</returns>
    public async Task<ConferenceInfo> GetConferenceAsync(JwtToken accessToken, string conferenceId, bool liveStats = false)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v1/monitor/conferences/${conferenceId}?livestats=${liveStats}";
        return await _httpClient.SendGetAsync<ConferenceInfo>(url, accessToken);
    }

    /// <summary>
    /// Get statistics of a terminated conference.
    /// The statistics include the maximum number of participants present during a conference
    /// and the maximum number of the transmitted and received packets, bytes, and streams.
    /// Note: Only terminated conferences include a complete summary.<br />
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-conference-statistics"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">The conference identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="ConferenceDetails" /> object.</returns>
    public async Task<ConferenceStatistics> GetConferenceStatisticsAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v1/monitor/conferences/${conferenceId}/statistics";
        return await _httpClient.SendGetAsync<ConferenceStatistics>(url, accessToken);
    }

    /// <summary>
    /// Get statistics and connection details of all participants in a conference.
    /// Optionally limit the search result with a specific time range.<br />
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-info-conference-participants"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the conference participants.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="GetConferenceParticipantsResponse" /> object.</returns>
    public async Task<GetConferenceParticipantsResponse> GetConferenceParticipantsAsync(JwtToken accessToken, GetConferenceParticipantsOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = $"/v1/monitor/conferences/{options.ConferenceId}/participants";
        if (!string.IsNullOrWhiteSpace(options.UserId))
            uriBuilder.Path += $"/{options.UserId}";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.Max ?? 100).ToString());
        if (!string.IsNullOrWhiteSpace(options.Type))
            nvc.Add("type", options.Type);
        if (!string.IsNullOrWhiteSpace(options.Start))
            nvc.Add("start", options.Start);

        uriBuilder.Query = nvc.ToString();

        return await _httpClient.SendPostAsync<GetConferenceParticipantsResponse>(uriBuilder.Uri.ToString(), accessToken);
    }

    /// <summary>
    /// Get statistics and connection details of all participants in a conference.
    /// Optionally limit the search result with a specific time range.<br />
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-info-conference-participants"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the conference participants.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of <see cref="ConferenceDetails" /> objects.</returns>
    public async Task<IEnumerable<ConferenceParticipant>> GetAllConferenceParticipantsAsync(JwtToken accessToken, GetAllConferenceParticipantsOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.CAPI_BASE_URL);
        uriBuilder.Path = $"/v1/monitor/conferences/{options.ConferenceId}/participants";
        if (!string.IsNullOrWhiteSpace(options.UserId))
            uriBuilder.Path += $"/{options.UserId}";

        var nvc = new NameValueCollection();
        nvc.Add("from", (options.From ?? 0).ToString());
        nvc.Add("to", (options.To ?? 9999999999999).ToString());
        nvc.Add("max", (options.PageSize ?? 100).ToString());
        if (!string.IsNullOrWhiteSpace(options.Type))
            nvc.Add("type", options.Type);

        uriBuilder.Query = nvc.ToString();

        List<ConferenceParticipant> result = new List<ConferenceParticipant>();

        GetConferenceParticipantsResponse response;
        do
        {
            response = await _httpClient.SendPostAsync<GetConferenceParticipantsResponse>(uriBuilder.Uri.ToString(), accessToken);
            result.AddRange(response.Participants.Values);

            nvc.Add("start", response.Next);
            uriBuilder.Query = nvc.ToString();
        } while (!string.IsNullOrWhiteSpace(response.Next));

        return result;
    }
}
