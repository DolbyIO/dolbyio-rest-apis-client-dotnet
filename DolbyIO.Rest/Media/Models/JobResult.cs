using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class ErrorResult
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("details")]
    public string Details { get; set; }
}

public abstract class JobResult<TResult>
{
    [JsonProperty("api_version")]
    public string ApiVersion { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("progress")]
    public int Progress { get; set; }

    [JsonProperty("result")]
    public TResult Result { get; set; }

    [JsonProperty("error")]
    public ErrorResult Error { get; set; }
}
