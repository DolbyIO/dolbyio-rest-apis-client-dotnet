using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest;

public sealed class Authentication
{
    private readonly HttpClient _httpClient;

    internal Authentication(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Generates an API token.
    /// To make any API call, you must acquire a JWT (JSON Web Token) format API token.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-api-token"/><br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/get-api-token"/>
    /// </summary>
    /// <param name="appKey">Your Dolby.io App Key.</param>
    /// <param name="appSecret">Your Dolby.io App Secret.</param>
    /// <param name="expiresIn">API token expiration time in seconds. If no value is specified, the default is 1800, indicating 30 minutes. The maximum value is 86,400, indicating 24 hours.</param>
    /// <param name="scope">A list of case-sensitive strings allowing you to control what scope of access the API token should have. If not specified, the API token will possess unrestricted access to all resources and actions. The API supports the following scopes:
    /// <list type="bullet">
    ///     <item>
    ///         <term>comms:client_access_token:create</term>
    ///         <description>Allows requesting a client access token.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:conf:create</term>
    ///         <description>Allows creating a new conference.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:conf:admin</term>
    ///         <description>Allows administrating a conference, including actions such as Invite, Kick, Send Message, Set Spatial Listener's Audio, and Update Permissions.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:conf:destroy</term>
    ///         <description>Allows terminating a live conference.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:monitor:delete</term>
    ///         <description>Allows deleting data from the Monitor API, for example, deleting recordings.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:monitor:read</term>
    ///         <description>Allows reading data through the Monitor API.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:monitor:download</term>
    ///         <description>Allows generating download URLs for data (e.g.recording) through the Monitor API.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:stream:write</term>
    ///         <description>Allows starting and stopping RTMP or Real-Time streaming.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:remix:write</term>
    ///         <description>Allows remixing recordings.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:remix:read</term>
    ///         <description>Allows reading the remix status.</description>
    ///     </item>
    ///     <item>
    ///         <term>comms:record:write</term>
    ///         <description>Allows starting and stopping recordings.</description>
    ///     </item>
    /// </list>
    /// Incorrect values are omitted.If you want to give the token access to all Communications REST APIs, you can use a wildcard, such as comms:*</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the newly created <see cref="JwtToken" />.</returns>
    public async Task<JwtToken> GetApiAccessTokenAsync(string appKey, string appSecret, int expiresIn = 1800, string[] scope = null)
    {
        var nvc = new Dictionary<string, string>() {
            { "grant_type", "client_credentials" },
            { "expires_in", expiresIn.ToString() }
        };
        if (scope != null && scope.Length > 0)
        {
            nvc.Add("scope", string.Join(' ', scope));
        }

        const string url = Urls.API_BASE_URL + "/v1/auth/token";
        string authz = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{appKey}:{appSecret}"));
        using (var request = new HttpRequestMessage(HttpMethod.Post, url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authz);
            request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };

            request.Content = new FormUrlEncodedContent(nvc);

            return await _httpClient.SendAsync<JwtToken>(request);
        }
    }
}
