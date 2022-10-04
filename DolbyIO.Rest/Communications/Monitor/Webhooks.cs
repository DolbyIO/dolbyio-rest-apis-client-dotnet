using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;

namespace DolbyIO.Rest.Communications.Monitor
{
    public sealed class Webhooks
    {
        private readonly HttpClient _httpClient;

        internal Webhooks(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets a list of Webhook events sent, during a specific time range.
        /// The list includes associated endpoint response codes and headers.<br/>
        /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-webhooks"/>
        /// </summary>
        /// <param name="accessToken">Access token to use for authentication.</param>
        /// <param name="options">Options to request the webhooks.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="GetWebHookResponse" /> object.</returns>
        public async Task<GetWebHookResponse> GetEventsAsync(JwtToken accessToken, GetWebhooksOptions options)
        {
            var uriBuilder = new UriBuilder(Urls.COMMS_BASE_URL);

            uriBuilder.Path = "/v1/monitor/";
            if (!string.IsNullOrWhiteSpace(options.ConferenceId))
                uriBuilder.Path += $"conferences/{options.ConferenceId}/";
            uriBuilder.Path += "webhooks";

            var nvc = new NameValueCollection();
            nvc.Add("from", (options.From ?? 0).ToString());
            nvc.Add("to", (options.To ?? 9999999999999).ToString());
            nvc.Add("max", (options.Max ?? 100).ToString());
            if (!string.IsNullOrWhiteSpace(options.Type))
                nvc.Add("type", options.Type);
            if (!string.IsNullOrWhiteSpace(options.Start))
                nvc.Add("start", options.Start);

            uriBuilder.Query = nvc.ToString();

            return await _httpClient.SendPostAsync<GetWebHookResponse>(uriBuilder.Uri.ToString(), accessToken);
        }

        /// <summary>
        /// Gets a list of all Webhook events sent, during a specific time range.
        /// The list includes associated endpoint response codes and headers.<br/>
        /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-webhooks"/>
        /// </summary>
        /// <param name="accessToken">Access token to use for authentication.</param>
        /// <param name="options">Options to request the webhooks.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of <see cref="WebHook" /> objects.</returns>
        public async Task<IEnumerable<WebHook>> GetAllEventsAsync(JwtToken accessToken, GetAllWebhooksOptions options)
        {
            var uriBuilder = new UriBuilder(Urls.COMMS_BASE_URL);

            uriBuilder.Path = "/v1/monitor/";
            if (!string.IsNullOrWhiteSpace(options.ConferenceId))
                uriBuilder.Path += $"conferences/{options.ConferenceId}/";
            uriBuilder.Path += "webhooks";

            var nvc = new NameValueCollection();
            nvc.Add("from", (options.From ?? 0).ToString());
            nvc.Add("to", (options.To ?? 9999999999999).ToString());
            nvc.Add("max", (options.PageSize ?? 100).ToString());
            if (!string.IsNullOrWhiteSpace(options.Type))
                nvc.Add("type", options.Type);

            uriBuilder.Query = nvc.ToString();

            List<WebHook> result = new List<WebHook>();

            GetWebHookResponse response;
            do
            {
                response = await _httpClient.SendPostAsync<GetWebHookResponse>(uriBuilder.Uri.ToString(), accessToken);
                result.AddRange(response.Webhooks);

                nvc.Add("start", response.Next);
                uriBuilder.Query = nvc.ToString();
            } while (!string.IsNullOrWhiteSpace(response.Next)); 

            return result;
        }
    }
}

