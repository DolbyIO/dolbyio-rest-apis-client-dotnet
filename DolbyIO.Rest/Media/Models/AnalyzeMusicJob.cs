using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class AnalyzeMusicJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class AnalyzeMusicJob : JobResult<AnalyzeMusicJobResult>
{
}
