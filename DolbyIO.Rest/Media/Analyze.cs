﻿using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;

namespace DolbyIO.Rest.Media;

public sealed class Analyze
{
    private readonly HttpClient _httpClient;

    internal Analyze(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Starts analyzing to learn about your media.<br/>
    /// The <c>input</c> location of your source media file and <c>output</c> location of your Analyze JSON results file are required.
    /// This is an asynchronous operation so you will receive a job identifier to be used to get the job status and result.
    /// There are additional optional parameters that can be provided to identify the type of content and additional loudness or validation requirements.See the samples for examples of what requests and responses look like.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-analyze-post"/>
    /// </summary>
    /// <remarks>
    /// Content Length - Media content with duration less than 2 seconds will not be processed.The API will return an ERROR in this case.
    /// </remarks>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobDescription">Content of the job description as a JSON payload.
    /// You can find the definition at this URL: <seealso cref="https://docs.dolby.io/media-apis/reference/media-analyze-post"/></param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the job identifier.</returns>
    public async Task<string> StartAsync(JwtToken accessToken, string jobDescription)
    {
        return await _httpClient.StartJobAsync(accessToken, "/media/analyze", jobDescription);
    }

    /// <summary>
    /// Gets Analyze Status.<br/>
    /// For a given job id, this method will check the job status.
    /// When <c>status == Success</c> you'll be able to retrieve your result from the output location you provided in the original POST.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-analyze-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">The job identifier.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="AnalyzeJob" /> object.</returns>
    public async Task<AnalyzeJob> GetStatusAsync(JwtToken accessToken, string jobId)
    {
        return await _httpClient.GetJobResultAsync<AnalyzeJob>(accessToken, "/media/analyze", jobId);
    }
}
