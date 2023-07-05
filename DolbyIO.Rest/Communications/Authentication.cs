using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Communications;

public sealed class Authentication
{
    private readonly HttpClient _httpClient;

    internal Authentication(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets a client access token to authenticate a session.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-client-access-token"/>
    /// </summary>
    /// <param name="appKey">Your Dolby.io App Key.</param>
    /// <param name="appSecret">Your Dolby.io App Secret.</param>
    /// <param name="expiresIn">Access token expiration time in seconds. The maximum value is 86,400, indicating 24 hours. If no value is specified, the default is 3,600, indicating one hour.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the newly created <see cref="JwtToken" />.</returns>
    [Obsolete("This function is now deprecated and will be removed in the next release of this SDK. Please start using GetClientAccessTokenV2Async instead.")]
    public async Task<JwtToken> GetClientAccessTokenAsync(string appKey, string appSecret, int expiresIn = 3600)
    {
        var nvc = new Dictionary<string, string>() {
            { "grant_type", "client_credentials" },
            { "expires_in", expiresIn.ToString() }
        };

        const string url = Urls.SESSION_BASE_URL + "/v1/oauth2/token";
        string authz = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{appKey}:{appSecret}"));
        using (var request = new HttpRequestMessage(HttpMethod.Post, url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authz);
            request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };

            request.Content = new FormUrlEncodedContent(nvc);

            return await _httpClient.SendAsync<JwtToken>(request);
        }
    }

    /// <summary>
    /// Gets a client access token to authenticate a session.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-client-access-token"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="sessionScope">A list of case-sensitive strings allowing you to control what scope of access the client access token should have. The API supports the following scopes:
    /// <list type="bullet">
    ///     <item>
    ///         <term>conf:create</term>
    ///         <description>Allows creating a new conference.</description>
    ///     </item>
    ///     <item>
    ///         <term>notifications:set</term>
    ///         <description>Allows the client to subscribe to events.</description>
    ///     </item>
    ///     <item>
    ///         <term>file:convert</term>
    ///         <description>Allows converting files.</description>
    ///     </item>
    ///     <item>
    ///         <term>session:update</term>
    ///         <description>Allows updating the participant's name and avatar URL.</description>
    ///     </item>
    /// </list>
    /// Incorrect values are omitted.If you want to give the token access to all scopes, you can use a wildcard, such as *.</param>
    /// <param name="externalId">The unique identifier of the participant who requests the token.</param>
    /// <param name="expiresIn">Access token expiration time in seconds. If no value is specified, the default is 3,600, indicating one hour. The maximum value is 86,400, indicating 24 hours.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the newly created <see cref="JwtToken" />.</returns>
    public async Task<JwtToken> GetClientAccessTokenV2Async(JwtToken accessToken, string[] sessionScope, string externalId = null, int expiresIn = 3600)
    {
        var nvc = new Dictionary<string, string>() {
            { "grant_type", "client_credentials" },
            { "expires_in", expiresIn.ToString() }
        };
        if (sessionScope != null && sessionScope.Length > 0)
        {
            nvc.Add("sessionScope", string.Join(' ', sessionScope));
        }

        const string url = Urls.CAPI_BASE_URL + "/v2/client-access-token";
        return await _httpClient.SendPostAsync<dynamic, JwtToken>(url, accessToken, nvc);
    }
}
