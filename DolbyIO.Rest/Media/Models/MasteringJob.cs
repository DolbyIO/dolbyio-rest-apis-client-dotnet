using System;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class MasteringPreviewJobResult
{
    [JsonProperty("media_info")]
    public object MediaInfo { get; internal set; }

    [JsonProperty("audio")]
    public object Audio { get; internal set; }
}

public sealed class MasteringPreviewJob : JobResult<MasteringPreviewJobResult>
{
}

public sealed class MasteringJobResult
{
    /// <summary>
    /// Gets the level of the media prior to mastering.
    /// Measured in loudness units relative to full scale (LUFS).
    /// </summary>
    [JsonProperty("initial_level")]
    public string InitialLevel { get; internal set; }

    /// <summary>
    /// Gets the level of the media after mastering.
    /// Measured in loudness units relative to full scale (LUFS).
    /// </summary>
    [JsonProperty("final_level")]
    public string FinalLevel { get; internal set; }
}

public sealed class MasteringJob : JobResult<MasteringJobResult>
{
}
