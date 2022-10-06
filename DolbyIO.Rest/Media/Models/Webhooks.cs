using System.Collections.Specialized;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

internal sealed class WebhookCreationResult
{
    [JsonProperty("webhook_id")]
    public string WebhookId { get; internal set; }
}

public sealed class UpdateWebhookOptions
{
    /// <summary>
    /// Gets or sets the <see cref="WebhookId"/> returned from a previous GET, POST or PUT response to retrieve the webhook configuration.
    /// </summary>
    public string WebhookId { get; set; }

    /// <summary>
    /// Gets or sets the callback url that will be called when job execution completes.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the headers to include in the webhook call.
    /// </summary>
    public NameValueCollection Headers { get; set; }
}

public sealed class WebhookCallback
{
    /// <summary>
    /// Gets or sets the callback url that will be called when job execution completes.
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; internal set; }

    /// <summary>
    /// Gets or sets the headers to include in the webhook call.
    /// </summary>
    [JsonProperty("headers")]
    public NameValueCollection Headers { get; internal set; }
}

public sealed class Webhook
{
    /// <summary>
    /// Gets or sets the <see cref="WebhookId"/> returned from a previous GET, POST or PUT response to retrieve the webhook configuration.
    /// </summary>
    [JsonProperty("webhook_id")]
    public string WebhookId { get; internal set; }

    [JsonProperty("callback")]
    public WebhookCallback Callback { get; internal set; }
}
