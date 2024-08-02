using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media;

public sealed class Enhance
{
    private readonly HttpClient _httpClient;

    internal Enhance(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts enhancing to improve your media.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// This is an asynchronous operation so you will receive a job identifier where you can retrieve the results when enhancement is complete.
    /// There are additional optional parameters that can be provided to control and select the type of enhancements made.
    /// See the samples for some examples of what requests and responses look like.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-enhance-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">The job description.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, EnhanceJobDescription jobDescription)
    {
        string strJobDescription = JsonConvert.SerializeObject(jobDescription);
        return await StartAsync(accessToken, strJobDescription);
    }

    /// <summary>
    /// Starts enhancing to improve your media.<br/>
    /// The <c>input</c> location of your source media file is required.
    /// This is an asynchronous operation so you will receive a job identifier where you can retrieve the results when enhancement is complete.
    /// There are additional optional parameters that can be provided to control and select the type of enhancements made.
    /// See the samples for some examples of what requests and responses look like.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-enhance-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-enhance-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/enhance", jobDescription);
    }

    /// <summary>
    /// Gets Enhance Results.<br/>
    /// For a given job id, this method will check if the processing task has completed. The <c>Progress</c> property provides a percentage of job progress.
    /// When the status is Success you'll be able to retrieve your result from the output location you provided in the original POST.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-enhance-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="EnhanceJob" /> object.</returns>
    public async Task<EnhanceJob> GetResultsAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<EnhanceJob>(accessToken, "/media/enhance", jobId);
    }
}
