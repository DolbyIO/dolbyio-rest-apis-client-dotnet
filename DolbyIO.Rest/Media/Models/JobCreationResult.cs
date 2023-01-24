using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

internal sealed class JobCreationResult
{
    [JsonProperty("job_id")]
    public string JobId { get; set; }
}
