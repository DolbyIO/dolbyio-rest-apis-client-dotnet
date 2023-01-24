using System;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class AnalyzeSpeechJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class AnalyzeSpeechJob : JobResult<AnalyzeSpeechJobResult>
{
}
