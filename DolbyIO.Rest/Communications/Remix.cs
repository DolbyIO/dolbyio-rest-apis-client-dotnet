using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Communications;

public sealed class Remix
{
    private readonly HttpClient _httpClient;

    internal Remix(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Creates a conference.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/start-conference-remix"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="layoutUrl">
    /// Overwrites the layout URL configuration:
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>null</c></term>
    ///         <description>uses the layout URL configured in the dashboard (if no URL is set in the dashboard, then uses the Dolby.io default).</description>
    ///     </item>
    ///     <item>
    ///         <term><c>default</c></term>
    ///         <description>uses the Dolby.io default layout.</description>
    ///     </item>
    ///     <item>
    ///         <term>URL string</term>
    ///         <description>uses this layout URL.</description>
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="layoutName">Defines a name for the given layout URL, which makes layout identification easier for customers especially when the layout URL is not explicit.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="RemixStatus" /> object.</returns>
    public async Task<RemixStatus> StartAsync(JwtToken accessToken, string conferenceId, string layoutUrl = null, string layoutName = null)
    {
        var body = new {
            layoutUrl = layoutUrl,
            layoutName = layoutName
        };
        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/remix/start";
        return await _httpClient.SendPostAsync<dynamic, RemixStatus>(url, accessToken, body);
    }

    /// <summary>
    /// Gets the status of a current mixing job.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/get-conference-remix-status"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="RemixStatus" /> object.</returns>
    public async Task<RemixStatus> GetStatusAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/mix/{conferenceId}/remix/start";
        return await _httpClient.SendGetAsync<RemixStatus>(url, accessToken);
    }
}
