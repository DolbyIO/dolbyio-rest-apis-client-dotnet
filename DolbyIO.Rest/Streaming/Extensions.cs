using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

internal static class Extensions
{
    public static HttpRequestMessage BuildHttpRequestMessageBase(
        HttpMethod httpMethod,
        string url,
        string bearerToken,
        string accept = "application/json")
    {
        var request = new HttpRequestMessage(httpMethod, url);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };

        return request;
    }

    public static HttpRequestMessage BuildHttpRequestMessage(
        HttpMethod httpMethod,
        string url,
        string bearerToken,
        string content,
        string contentType = "application/json",
        string accept = "application/json")
    {
        HttpRequestMessage request = BuildHttpRequestMessageBase(httpMethod, url, bearerToken, accept);

        request.Content = new StringContent(content, Encoding.UTF8, contentType);

        return request;
    }

    public static async Task<string> SendRequestAsync(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
    {
        using HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);

        string json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);
            throw new Exception($"Code: {error.Code}\nStatus: {error.Status}\nMessage: {error.Message}\nData: {error.Data}");
        }

        return json;
    }

    public static async Task<TResult> GetResponseAsync<TResult>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        where TResult : class
    {
        string json = await SendRequestAsync(httpClient, httpRequestMessage);
        return JsonConvert.DeserializeObject<TResult>(json);
    }
}
