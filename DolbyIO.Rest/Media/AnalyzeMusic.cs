using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;

namespace DolbyIO.Rest.Media;

public sealed class AnalyzeMusic
{
    private readonly HttpClient _httpClient;

    internal AnalyzeMusic(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts analyzing to learn about music in your media.<br/>
    /// The <c>input</c> location of your source media file and <c>output</c> location of your Analyze JSON results file are required.
    /// This is an asynchronous operation so you will receive a job identifier to be used to get the job status and result.
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-analyze-music-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-analyze-music-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/analyze/music", jobDescription);
    }

    /// <summary>
    /// Gets Music Analytics Status.<br/>
    /// For a given job_id, this method will check the job status.
    /// When <c>status == Success</c> you'll be able to retrieve your result from the output location you provided in the original POST.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-analyze-music-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="AnalyzeMusicJob" /> object.</returns>
    public async Task<AnalyzeMusicJob> GetStatusAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<AnalyzeMusicJob>(accessToken, "/media/analyze/music", jobId);
    }
}
