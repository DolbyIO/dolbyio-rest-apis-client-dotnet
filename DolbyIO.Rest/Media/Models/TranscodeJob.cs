using System;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class TranscodeJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class TranscodeJob : JobResult<TranscodeJobResult>
{
}
