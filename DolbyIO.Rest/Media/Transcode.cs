using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Media;

public sealed class Transcode
{
    private readonly HttpClient _httpClient;

    internal Transcode(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts Transcoding.<br/>
    /// Start transcoding to modify the size, bitrates, and formats for your media.
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-transcode-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-transcode-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/transcode", jobDescription);
    }

    /// <summary>
    /// Gets Transcode Results.<br/>
    /// For a given job id, this method will check if the processing task has completed. The <c>Progress</c> property provides a percentage of job progress.
    /// When the status is Success you'll be able to retrieve your result from the output location you provided in the original POST.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-transcode-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="TranscodeJob" /> object.</returns>
    public async Task<TranscodeJob> GetResultsAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<TranscodeJob>(accessToken, "/media/transcode", jobId);
    }
}
