using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Media;

public sealed class Io
{
    private readonly HttpClient _httpClient;

    internal Io(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets an upload URL.<br/>
    /// To use the Dolby provided temporary storage is a two step process.<br/>
    /// You start by declaring a dlb:// url that you can reference in any other Media API calls.
    /// The response will provide a url where you can put your media.
    /// This allows you to use the dlb:// url as a short-cut for a temporary storage location.<br/>
    /// You'll be returned a pre-signed url you can use to PUT and upload your media file.
    /// The temporary storage should allow you to read and write to the dlb:// locations for a period of at least 24 hours before it is removed.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-input"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="dlbUrl">The <c>url</c> should be in the form <c>dlb://object-key</c> where the object-key can be any alpha-numeric string.
    /// The object-key is unique to your account API Key so there is no risk of collision with other users.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the upload URL.</returns>
    public async Task<string> GetUploadUrlAsync(JwtToken accessToken, string dlbUrl)
    {
        var body = new { url = dlbUrl };
        const string requestUrl = Urls.CAPI_BASE_URL + "/media/input";
        GetUploadUrlResponse result = await _httpClient.SendPostAsync<dynamic, GetUploadUrlResponse>(requestUrl, accessToken, body);
        return result.Url;
    }

    /// <summary>
    /// Gets the download URL.<br/>
    /// To use the Dolby provided temporary storage is a two step process.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-input"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="dlbUrl">The <c>url</c> should be in the form <c>dlb://object-key</c> where the object-key can be any alpha-numeric string.
    /// The object-key is unique to your account API Key so there is no risk of collision with other users.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the download URL.</returns>
    public async Task<string> GetDownloadUrlAsync(JwtToken accessToken, string dlbUrl)
    {
        var body = new { url = dlbUrl };
        const string requestUrl = Urls.CAPI_BASE_URL + "/media/output";
        GetDownloadUrlResponse result = await _httpClient.SendPostAsync<dynamic, GetDownloadUrlResponse>(requestUrl, accessToken, body);
        return result.Url;
    }
}
