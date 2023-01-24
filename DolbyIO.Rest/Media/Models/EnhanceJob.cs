using System;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class EnhanceJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class EnhanceJob : JobResult<EnhanceJobResult>
{
}
