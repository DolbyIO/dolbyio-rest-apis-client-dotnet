using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

internal sealed class GetUploadUrlResponse
{
    [JsonProperty("url")]
    public string Url { get; set; }
}

internal sealed class GetDownloadUrlResponse
{
    [JsonProperty("url")]
    public string Url { get; set; }
}
