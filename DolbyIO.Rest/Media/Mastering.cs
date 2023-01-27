using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media;

public sealed class Mastering
{
    private readonly HttpClient _httpClient;

    internal Mastering(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts mastering preview to improve your music.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// A <c>preset</c> applies dynamic EQ processing to shape your music to match a desired sound.
    /// There are also additional optional parameters that can be provided to control the mastering output.
    /// A <c>segment</c> object specifying preview <c>start</c> may optionally be provided.
    /// This is an asynchronous operation. You receive a job identifier that you use to retrieve the results when the mastering is complete.
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-preview-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">The job description.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartPreviewAsync(JwtToken accessToken, MasteringPreviewJobDescription jobDescription)
    {
        string strJobDescription = JsonConvert.SerializeObject(jobDescription);
        return await StartPreviewAsync(accessToken, strJobDescription);
    }

    /// <summary>
    /// Starts mastering preview to improve your music.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// A <c>preset</c> applies dynamic EQ processing to shape your music to match a desired sound.
    /// There are also additional optional parameters that can be provided to control the mastering output.
    /// A <c>segment</c> object specifying preview <c>start</c> may optionally be provided.
    /// This is an asynchronous operation. You receive a job identifier that you use to retrieve the results when the mastering is complete.
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-preview-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-preview-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartPreviewAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/master/preview", jobDescription);
    }

    /// <summary>
    /// Gets Mastering Preview Results.<br/>
    /// For a given job id, this method will check if the processing task has completed. The <c>Progress</c> property provides a percentage of job progress.
    /// When the status is Success you'll be able to retrieve your result from the output location you provided in the original POST.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-preview-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="MasteringPreviewJob" /> object.</returns>
    public async Task<MasteringPreviewJob> GetPreviewResultsAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<MasteringPreviewJob>(accessToken, "/media/master/preview", jobId);
    }

    /// <summary>
    /// Starts mastering to improve your music.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// A <c>preset</c> applies dynamic EQ processing to shape your music to match a desired sound.
    /// There are also additional optional parameters that can be provided to control the mastering output.
    /// This is an asynchronous operation. You receive a job identifier that you use to retrieve the results when the mastering is complete.
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">The job description.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, MasteringJobDescription jobDescription)
    {
        string strJobDescription = JsonConvert.SerializeObject(jobDescription);
        return await StartAsync(accessToken, strJobDescription);
    }

    /// <summary>
    /// Starts mastering to improve your music.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// A <c>preset</c> applies dynamic EQ processing to shape your music to match a desired sound.
    /// There are also additional optional parameters that can be provided to control the mastering output.
    /// This is an asynchronous operation. You receive a job identifier that you use to retrieve the results when the mastering is complete.
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/master", jobDescription);
    }

    /// <summary>
    /// Gets Mastering Results.<br/>
    /// For a given job id, this method will check if the processing task has completed. The <c>Progress</c> property provides a percentage of job progress.
    /// When the status is Success you'll be able to retrieve your result from the output location you provided in the original POST.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-music-mastering-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="MasteringJob" /> object.</returns>
    public async Task<MasteringJob> GetResultsAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<MasteringJob>(accessToken, "/media/master", jobId);
    }
}
