using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models
{
    public sealed class GetWebhooksOptions : PagedOptions
    {
        /// <summary>
        /// Gets or sets the identifier of the conference.
        /// </summary>
        public string ConferenceId { get; set; }

        /// <summary>
        /// Gets or sets the Webhook event type or an expression of its type (for example `Recording.Live.InProgress` or `Rec.*`).
        /// The default value of the type parameter returns all types of Webhooks.
        /// </summary>
        public string Type { get; set; }
    }

    public sealed class GetAllWebhooksOptions : AllElementsOptions
    {
        /// <summary>
        /// Gets or sets the identifier of the conference.
        /// </summary>
        public string ConferenceId { get; set; }

        /// <summary>
        /// Gets or sets the Webhook event type or an expression of its type (for example `Recording.Live.InProgress` or `Rec.*`).
        /// The default value of the type parameter returns all types of Webhooks.
        /// </summary>
        public string Type { get; set; }
    }

    public sealed class WebHook
    {
        /// <summary>
        /// Gets or sets the ID for the webhook event.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the string encoding the webhook JSON body.
        /// </summary>
        [JsonProperty("webhook")]
        public string Webhook { get; set; }

        /// <summary>
        /// Gets or sets the URL (configured on the developer portal) to which the webhook is sent.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the ID of the conference that emitted the webhook event.
        /// </summary>
        [JsonProperty("confId")]
        public string ConferenceId { get; set; }

        /// <summary>
        /// Gets or sets the app key of the application associated with the conference that emitted the webhook.
        /// </summary>
        [JsonProperty("thirdPartyId")]
        public string AppKey { get; set; }

        /// <summary>
        /// Gets or sets the epoch time of the webhook event.
        /// </summary>
        [JsonProperty("ts")]
        public string Ts { get; set; }

        /// <summary>
        /// Gets or sets the response from the endpoint at which the webhook event is posted.
        /// </summary>
        [JsonProperty("response")]
        public WebHookResponse Response { get; set; }
    }

    public sealed class WebHookResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code and the returned string after posting the webhook (for example "200 OK").
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the key - a value of the map of headers of the endpoint response returned after posting the webhook.
        /// </summary>
        [JsonProperty("headers")]
        public object Headers { get; set; }
    }

    public sealed class GetWebHookResponse : PagedResponse
    {
        /// <summary>
        /// Gets or sets the list of webhooks.
        /// </summary>
        [JsonProperty("webhooks")]
        public IEnumerable<WebHook> Webhooks { get; set; }
    }
}

