using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class AnalyzeJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class AnalyzeJob : JobResult<AnalyzeJobResult>
{
}
