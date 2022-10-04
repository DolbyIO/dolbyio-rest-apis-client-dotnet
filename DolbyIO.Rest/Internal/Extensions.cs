using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest
{
    internal static class Extensions
    {
        public static async Task<TResult> SendPostAsync<TResult>(this HttpClient httpClient, string url, JwtToken accessToken)
            where TResult : class
        {
            using (HttpResponseMessage response = await SendAsync(httpClient, HttpMethod.Post, url, accessToken, "{}"))
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(json);
            }
        }

        public static async Task<TResult> SendPostAsync<TContent, TResult>(this HttpClient httpClient, string url, JwtToken accessToken, TContent content)
            where TResult : class
            where TContent : class
        {
            string strContent = content == null ? "{}" : JsonConvert.SerializeObject(content);
            using (HttpResponseMessage response = await SendAsync(httpClient, HttpMethod.Post, url, accessToken, "{}"))
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(json);
            }
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
            using (HttpResponseMessage response = await SendAsync(httpClient, HttpMethod.Get, url, accessToken))
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(json);
            }
        }

        private static async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, JwtToken accessToken, string content = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.AccessToken);
                request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
                request.Headers.TryAddWithoutValidation("Accept", "application/json");

                if (!string.IsNullOrWhiteSpace(content))
                {
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                }

                return await httpClient.SendAsync(request);
            }
        }
    }
}

