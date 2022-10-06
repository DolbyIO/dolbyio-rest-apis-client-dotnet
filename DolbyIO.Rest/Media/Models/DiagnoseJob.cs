using System;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class DiagnoseJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class DiagnoseJob : JobResult<DiagnoseJobResult>
{
}
