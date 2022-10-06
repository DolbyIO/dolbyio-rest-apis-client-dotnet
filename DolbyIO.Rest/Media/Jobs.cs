using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Media;

public sealed class Jobs
{
    private readonly HttpClient _httpClient;

    internal Jobs(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Query Media Jobs.
    /// List of jobs previously submitted, up to the last 31 days.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-jobs-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the list of jobs.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="JobsResponse" /> object.</returns>
    public async Task<JobsResponse> ListAsync(JwtToken accessToken, ListJobsOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.MAPI_BASE_URL);
        uriBuilder.Path = "/media/jobs";

        var nvc = new NameValueCollection();
        if (!string.IsNullOrWhiteSpace(options.SubmittedAfter))
            nvc.Add("submitted_after", options.SubmittedAfter);
        if (!string.IsNullOrWhiteSpace(options.SubmittedBefore))
            nvc.Add("submitted_before", options.SubmittedBefore);
        if (!string.IsNullOrWhiteSpace(options.Status))
            nvc.Add("status", options.Status);
        if (!string.IsNullOrWhiteSpace(options.NextToken))
            nvc.Add("next_token", options.NextToken);
        uriBuilder.Query = nvc.ToString();

        return await _httpClient.SendGetAsync<JobsResponse>(uriBuilder.ToString(), accessToken);
    }

    /// <summary>
    /// Query Media Jobs.
    /// List of all jobs previously submitted, up to the last 31 days.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-jobs-get"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to request the list of jobs.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of <see cref="Job" /> objects.</returns>
    public async Task<IEnumerable<Job>> ListAllAsync(JwtToken accessToken, ListAllJobsOptions options)
    {
        var uriBuilder = new UriBuilder(Urls.MAPI_BASE_URL);
        uriBuilder.Path = "/media/jobs";

        var nvc = new NameValueCollection();
        if (!string.IsNullOrWhiteSpace(options.SubmittedAfter))
            nvc.Add("submitted_after", options.SubmittedAfter);
        if (!string.IsNullOrWhiteSpace(options.SubmittedBefore))
            nvc.Add("submitted_before", options.SubmittedBefore);
        if (!string.IsNullOrWhiteSpace(options.Status))
            nvc.Add("status", options.Status);
        uriBuilder.Query = nvc.ToString();

        List<Job> result = new List<Job>();

        JobsResponse response;
        do
        {
            response = await _httpClient.SendPostAsync<JobsResponse>(uriBuilder.Uri.ToString(), accessToken);
            result.AddRange(response.Jobs);

            nvc.Add("next_token", response.NextToken);
            uriBuilder.Query = nvc.ToString();
        } while (!string.IsNullOrWhiteSpace(response.NextToken));

        return result;
    }

    /// <summary>
    /// Requests cancellation of a previously submitted job.<br/>
    /// See: <seealso cref="https://docs.dolby.io/media-apis/reference/media-jobs-cancel-post"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="jobId">Identifier of the job to cancel.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task CancelAsync(JwtToken accessToken, string jobId)
    {
        var uriBuilder = new UriBuilder(Urls.MAPI_BASE_URL);
        uriBuilder.Path = "/media/jobs/cancel";

        var nvc = new NameValueCollection();
        nvc.Add("job_id", jobId);
        uriBuilder.Query = nvc.ToString();

        await _httpClient.SendPostAsync(uriBuilder.ToString(), accessToken);
    }
}
