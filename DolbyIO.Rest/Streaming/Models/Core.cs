using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public abstract class BaseResponse<TData>
{
    [JsonProperty("status")]
    public string Status { get; internal set; }

    [JsonProperty("data")]
    public TData Data { get; internal set; }
}

public sealed class ErrorResponse : BaseResponse<string>
{
    [JsonProperty("code")]
    public int Code { get; internal set; }

    [JsonProperty("message")]
    public string Message { get; internal set; }
}
