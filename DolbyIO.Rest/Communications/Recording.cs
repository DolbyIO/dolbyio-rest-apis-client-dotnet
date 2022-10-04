using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;

namespace DolbyIO.Rest.Communications
{
    public sealed class Recording
    {
        private readonly HttpClient _httpClient;

        internal Recording(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Starts recording for the specified conference.
        /// You can specify a custom layout URL per recording request.
        /// The <c>layoutURL</c> parameter overrides the layout URL configured in the dashboard.<br/>
        /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/api-recording-start"/>
        /// </summary>
        /// <param name="accessToken">Access token to use for authentication.</param>
        /// <param name="conferenceId">Identifier of the conference.</param>
        /// <param name="layoutUrl">
        /// Overwrites the layout URL configuration.
        /// This field is ignored if it is not relevant regarding recording configuration,
        /// for example if <c>live_recording</c> set to false or if the recording is MP3 only.
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
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StartAsync(JwtToken accessToken, string conferenceId, string layoutUrl = null)
        {
            var requestObject = new StartRecordingRequest { LayoutUrl = layoutUrl };

            string url = $"{Urls.COMMS_BASE_URL}/v2/conferences/mix/{conferenceId}/recording/start";
            await _httpClient.SendPostAsync(url, accessToken, requestObject);
        }

        /// <summary>
        /// Stops the recording of the specified conference.<br/>
        /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/api-recording-stop"/>
        /// </summary>
        /// <param name="accessToken">Access token to use for authentication.</param>
        /// <param name="conferenceId">Identifier of the conference.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StopAsync(JwtToken accessToken, string conferenceId)
        {
            string url = $"{Urls.COMMS_BASE_URL}/v2/conferences/mix/{conferenceId}/recording/stop";
            await _httpClient.SendPostAsync(url, accessToken);
        }
    }
}
