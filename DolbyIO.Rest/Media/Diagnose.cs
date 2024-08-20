using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;

namespace DolbyIO.Rest.Media;

public sealed class Diagnose
{
    private readonly HttpClient _httpClient;

    internal Diagnose(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts diagnosing to learn about the audio quality of your media.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// This is an asynchronous operation so you will receive a job identifier to be used to get the job status and result.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-diagnose-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-diagnose-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/diagnose", jobDescription);
    }

    /// <summary>
    /// Gets Diagnose Results.<br/>
    /// For a given job id, this method will check if the processing task has completed. The <c>Progress</c> property provides a percentage of job progress.
    /// If the <c>Status</c> is Success then the json result will be returned in the response.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-diagnose-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="DiagnoseJob" /> object.</returns>
    public async Task<DiagnoseJob> GetResultsAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<DiagnoseJob>(accessToken, "/media/diagnose", jobId);
    }
}
