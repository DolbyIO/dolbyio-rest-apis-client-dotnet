using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Media;

public sealed class Webhooks
{
    private readonly HttpClient _httpClient;

    internal Webhooks(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Registers a webhook that is triggered when a job completes.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-webhook-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="url">The callback url that will be called when job execution completes.</param>
    /// <param name="headers">Headers to include in the webhook call.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the webhook identifier.</returns>
    public async Task<string> RegisterAsync(JwtToken accessToken, string url, NameValueCollection headers = null)
    {
        var body = new {
            callback = new {
                url = url,
                headers = headers
            }
        };
        const string requestUrl = Urls.CAPI_BASE_URL + "/media/webhooks";
        WebhookCreationResult result = await _httpClient.SendPostAsync<dynamic, WebhookCreationResult>(requestUrl, accessToken, body);
        return result.WebhookId;
    }

    /// <summary>
    /// Updates the previously registered webhook configuration.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-webhook-put"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to update the webhook.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(JwtToken accessToken, UpdateWebhookOptions options)
    {
        var body = new
        {
            callback = new
            {
                url = options.Url,
                headers = options.Headers
            }
        };
        const string requestUrl = Urls.CAPI_BASE_URL + "/media/webhooks";
        await _httpClient.SendPutAsync(requestUrl, accessToken, body);
    }

    /// <summary>
    /// Retrieves the previously registered webhook configuration.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-webhook-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="webhookId">Identifier of the webhook to retrieve.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="Webhook" /> object.</returns>
    public async Task<Webhook> RetrieveAsync(JwtToken accessToken, string webhookId)
    {
        var uriBuilder = new UriBuilder(Urls.MAPI_BASE_URL);
        uriBuilder.Path = "/media/webhooks";

        var nvc = new NameValueCollection();
        nvc.Add("id", webhookId);
        uriBuilder.Query = nvc.ToString();

        return await _httpClient.SendGetAsync<Webhook>(uriBuilder.ToString(), accessToken);
    }

    /// <summary>
    /// Deletes a previously registered webhook configuration.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-webhook-delete"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="webhookId">Identifier of the webhook to delete.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(JwtToken accessToken, string webhookId)
    {
        var uriBuilder = new UriBuilder(Urls.MAPI_BASE_URL);
        uriBuilder.Path = "/media/webhooks";

        var nvc = new NameValueCollection();
        nvc.Add("id", webhookId);
        uriBuilder.Query = nvc.ToString();

        await _httpClient.SendDeleteAsync(uriBuilder.ToString(), accessToken);
    }
}
