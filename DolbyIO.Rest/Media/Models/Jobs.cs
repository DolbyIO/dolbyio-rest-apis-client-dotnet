using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public class ListAllJobsOptions
{
    /// <summary>
    /// Gets or sets the query jobs that were submitted at or after the specified date and time (inclusive).
    /// </summary>
    public string SubmittedAfter { get; set; }

    /// <summary>
    /// Gets or sets the query jobs that were submitted at or before the specified date and time (inclusive).
    /// The <see cref="SubmittedBefore"/> must be the same or later than <see cref="SubmittedAfter"/>.
    /// </summary>
    public string SubmittedBefore { get; set; }

    /// <summary>
    /// Gets or sets the query jobs that have the specified status.
    /// <c>Running</c>, <c>Pending</c>, <c>Success</c>, <c>Failed</c> or <c>InternalError</c>.
    /// </summary>
    public string Status { get; set; }
}

public sealed class ListJobsOptions : ListAllJobsOptions
{
    /// <summary>
    /// Used when querying the next page of jobs. Specify the <see cref="NextToken"/> that was returned in the previous call.
    /// </summary>
    public string NextToken { get; set; }
}

public sealed class Job
{
    [JsonProperty("job_id")]
    public string JobId { get; set; }

    [JsonProperty("api_version")]
    public string ApiVersion { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("progress")]
    public int Progress { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("time_submitted")]
    public string TimeSubmitted { get; set; }

    [JsonProperty("time_started")]
    public string TimeStarted { get; set; }

    [JsonProperty("time_completed")]
    public string TimeCompleted { get; set; }

    [JsonProperty("expiry")]
    public string Expiry { get; set; }
}

public sealed class JobsResponse
{
    [JsonProperty("jobs")]
    public IEnumerable<Job> Jobs { get; set; }

    [JsonProperty("next_token")]
    public string NextToken { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }
}
