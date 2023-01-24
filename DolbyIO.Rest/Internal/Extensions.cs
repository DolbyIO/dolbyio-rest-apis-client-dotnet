using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest;

internal static class Extensions
{
    public static async Task<TResult> SendPostAsync<TResult>(this HttpClient httpClient, string url, JwtToken accessToken, string content = "{}")
        where TResult : class
    {
        using HttpRequestMessage request = BuildHttpRequestMessage(HttpMethod.Post, url, accessToken, content);
        return await httpClient.SendAsync<TResult>(request);
    }

    public static async Task<TResult> SendPostAsync<TContent, TResult>(this HttpClient httpClient, string url, JwtToken accessToken, TContent content)
        where TResult : class
        where TContent : class
    {
        string strContent = content == null ? "{}" : JsonConvert.SerializeObject(content);
        using HttpRequestMessage request = BuildHttpRequestMessage(HttpMethod.Post, url, accessToken, strContent);
        return await httpClient.SendAsync<TResult>(request);
    }

    public static async Task SendPostAsync(this HttpClient httpClient, string url, JwtToken accessToken)
    {
        await SendAsync(httpClient, HttpMethod.Post, url, accessToken, "{}");
    }

    public static async Task SendPostAsync<TContent>(this HttpClient httpClient, string url, JwtToken accessToken, TContent content)
        where TContent : class
    {
        await SendAsync(httpClient, HttpMethod.Post, url, accessToken, JsonConvert.SerializeObject(content));
    }

    public static async Task SendPutAsync<TContent>(this HttpClient httpClient, string url, JwtToken accessToken, TContent content)
        where TContent : class
    {
        await SendAsync(httpClient, HttpMethod.Put, url, accessToken, JsonConvert.SerializeObject(content));
    }

    public static async Task SendDeleteAsync(this HttpClient httpClient, string url, JwtToken accessToken)
    {
        await SendAsync(httpClient, HttpMethod.Delete, url, accessToken);
    }

    public static async Task<TResult> SendGetAsync<TResult>(this HttpClient httpClient, string url, JwtToken accessToken)
        where TResult : class
    {
        using HttpRequestMessage request = BuildHttpRequestMessage(HttpMethod.Get, url, accessToken);
        return await httpClient.SendAsync<TResult>(request);
    }

    private static HttpRequestMessage BuildHttpRequestMessage(HttpMethod method, string url, JwtToken accessToken, string content = null)
    {
        var request = new HttpRequestMessage(method, url);

        request.Headers.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.AccessToken);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        request.Headers.TryAddWithoutValidation("Accept", "application/json");

        if (!string.IsNullOrWhiteSpace(content))
        {
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        }

        return request;
    }

    private static void SetUserAgent(this HttpRequestMessage httpRequestMessage)
    {
        Version libVersion = Assembly.GetAssembly(typeof(Extensions)).GetName().Version;
        string userAgent = $"DolbyIoRestApiSdk/{libVersion}; DotNet/{RuntimeInformation.FrameworkDescription}";
        httpRequestMessage.Headers.TryAddWithoutValidation("User-Agent", userAgent);
    }

    private static async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, JwtToken accessToken, string content = null)
    {
        using HttpRequestMessage request = BuildHttpRequestMessage(method, url, accessToken, content);

        // Add the user agent to the HTTP request
        request.SetUserAgent();

        return await httpClient.SendAsync(request);
    }

    public static async Task<TResult> SendAsync<TResult>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        where TResult : class
    {
        // Add the user agent to the HTTP request
        httpRequestMessage.SetUserAgent();

        using HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);
        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResult>(json);
    }
}
