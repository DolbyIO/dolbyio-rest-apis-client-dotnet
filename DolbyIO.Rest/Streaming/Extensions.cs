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
    public static HttpRequestMessage BuildHttpRequestMessage(
        HttpMethod httpMethod,
        string url,
        string apiSecret,
        string content,
        string contentType = "application/json",
        string accept = "application/json")
    {
        var request = new HttpRequestMessage(httpMethod, url);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiSecret);
        request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        request.Headers.TryAddWithoutValidation("Accept", accept);
        request.Content = new StringContent(content, Encoding.UTF8, contentType);

        return request;
    }

    public static async Task<TResult> GetResponseAsync<TResult>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        where TResult : class
    {
        using (HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage))
        {
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                throw new Exception($"Code: {error.Code}\nStatus: {error.Status}\nMessage: {error.Message}\nData: {error.Data}");
            }

            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
