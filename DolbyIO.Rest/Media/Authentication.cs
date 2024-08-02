using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;

namespace DolbyIO.Rest.Media;

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
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/get-api-token"/>
    /// </summary>
    /// <param name="appKey">Your Dolby.io App Key.</param>
    /// <param name="appSecret">Your Dolby.io App Secret.</param>
    /// <param name="expiresIn">API token expiration time in seconds. If no value is specified, the default is 1800, indicating 30 minutes. The maximum value is 86,400, indicating 24 hours.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the newly created <see cref="JwtToken" />.</returns>
    public async Task<JwtToken> GetApiAccessTokenAsync(string appKey, string appSecret, int expiresIn = 1800)
    {
        var nvc = new Dictionary<string, string>() {
            { "grant_type", "client_credentials" },
            { "expires_in", expiresIn.ToString() }
        };

        const string url = Urls.API_BASE_URL + "/v1/auth/token";
        string authz = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{appKey}:{appSecret}"));

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authz);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };

        request.Content = new FormUrlEncodedContent(nvc);

        return await _httpClient.SendAsync<JwtToken>(request);
    }
}
