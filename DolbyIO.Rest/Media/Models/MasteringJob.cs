using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DolbyIO.Rest.Media.Models;

public sealed class MasteringInputMetadata
{
    [JsonProperty("genre", NullValueHandling = NullValueHandling.Ignore)]
    public string Genre { get; set; }

    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> Tags { get; set; }
}

public class MasteringInput
{
    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public MasteringInputMetadata Metadata { get; }

    [JsonProperty("source")]
    public InputLocation Source { get; }

    public MasteringInput(InputLocation source, MasteringInputMetadata metadata)
    {
        Source = source;
        Metadata = metadata;
    }
}

public sealed class MasteringPreviewSegment
{
    [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
    public int? Start { get; }

    [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
    public int? Duration { get; }
}

public sealed class MasteringPreviewInput : MasteringInput
{
    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public MasteringPreviewSegment Segment { get; set; }

    public MasteringPreviewInput(InputLocation source, MasteringInputMetadata metadata)
        : base(source, metadata)
    {
    }
}

public sealed class MasteringOnComplete
{
    [JsonProperty("url")]
    public string Url { get; }

    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }

    [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary<string, string> Headers { get; set; }

    public MasteringOnComplete(string url)
    {
        Url = url;
    }
}

public enum DynamicEqPreset
{
    /// <summary>
    /// Tight dynamics and ample brightness in the upper frequencies. Suitable for pop music genres.
    /// </summary>
    a,
    /// <summary>
    /// Tight dynamics, a solid low-end, and mid-frequency clarity for vocal emphasis. Suitable for club music genres.
    /// </summary>
    b,
    /// <summary>
    /// Big bass presence and tight dynamics. Suitable for hip hop music genres.
    /// </summary>
    c,
    /// <summary>
    /// Heavy bass and sub-bass presence. Suitable for hip hop music genres.
    /// </summary>
    d,
    /// <summary>
    /// Big bass and sub-bass presence with additional openness in the mid and high frequencies. Suitable for hip hop and trap music genres.
    /// </summary>
    e,
    /// <summary>
    /// Wide low-end processing with a light and ethereal tone. Suitable for lighter electronic music genres.
    /// </summary>
    f,
    /// <summary>
    /// Wide low-end processing with dark and moody tone. Suitable for darker electronic music genres.
    /// </summary>
    g,
    /// <summary>
    /// Wide dynamics and ample openness in the mids and highs to allow for a wide spectrum of sound. Suitable for EDM music genres.
    /// </summary>
    h,
    /// <summary>
    /// Tight dynamics, and a well rounded, balanced tone. Suitable for diverse music genres.
    /// </summary>
    i,
    /// <summary>
    /// Smooth, tight dynamics, and a light lift in the upper frequencies. Suitable for rock music genres.
    /// </summary>
    j,
    /// <summary>
    /// Wide dynamics with a solid low and mid-frequency boost. Suitable for pop music genres.
    /// </summary>
    k,
    /// <summary>
    /// Emphasis on the mid-frequencies to highlight vocals. Suitable for lyrical or vocal-focused music genres.
    /// </summary>
    l,
    /// <summary>
    /// Light touch with ample mid-frequency clarity to let acoustic instruments shine in the mix. Suitable for acoustic music genres.
    /// </summary>
    m,
    /// <summary>
    /// Wide dynamics, and warm full tones for orchestral instruments. Suitable for classical music genres.
    /// </summary>
    n
}

public sealed class DynamicEq
{
    [JsonProperty("enable")]
    public bool Enable { get; }

    [JsonProperty("preset", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public DynamicEqPreset? Preset { get; }

    [JsonProperty("intensity", NullValueHandling = NullValueHandling.Ignore)]
    public int? Intensity { get; }

    /// <summary>
    /// The dynamic EQ settings for the master.
    /// </summary>
    /// <param name="enable">Option to disable dynamic EQ.</param>
    /// <param name="preset">Dynamic EQ preset to use for mastering.</param>
    /// <param name="intensity">Controls the amount of dynamic EQ processing applied to the music track.</param>
    public DynamicEq(bool enable, DynamicEqPreset? preset = null, int? intensity = null)
    {
        Enable = enable;
        Preset = preset;
        Intensity = intensity;
    }
}

public sealed class Loudness
{
    [JsonProperty("enable")]
    public bool Enable { get; }

    [JsonProperty("target_level", NullValueHandling = NullValueHandling.Ignore)]
    public int? TargetLevel { get; }

    /// <summary>
    /// The loudness settings for the master.
    /// </summary>
    /// <param name="enable">Option to disable loudness correction.</param>
    /// <param name="targetLevel">The integrated loudness target level. Measured in loudness units relative to full scale (LUFS).</param>
    public Loudness(bool enable, int? targetLevel = null)
    {
        Enable = enable;
        TargetLevel = targetLevel;
    }
}

public sealed class StereoImage
{
    [JsonProperty("enable")]
    public bool Enable { get; }

    /// <summary>
    /// The stereo image settings for the master.
    /// </summary>
    /// <param name="enable">Option to enable stereo image enhancement.</param>
    public StereoImage(bool enable)
    {
        Enable = enable;
    }
}

public sealed class MasterOutputMaster
{
    [JsonProperty("dynamic_eq", NullValueHandling = NullValueHandling.Ignore)]
    public DynamicEq DynamicEq { get; set; }

    [JsonProperty("loudness", NullValueHandling = NullValueHandling.Ignore)]
    public Loudness Loudness { get; set; }

    [JsonProperty("stereo_image", NullValueHandling = NullValueHandling.Ignore)]
    public StereoImage StereoImage { get; set; }
}

public class MasteringPreviewOutput
{
    [JsonProperty("destination")]
    public OutputLocation Destination { get; set; }

    [JsonProperty("master", NullValueHandling = NullValueHandling.Ignore)]
    public MasterOutputMaster Master { get; set; }
}

public enum MasterOutputKind
{
    Aac,
    Mp3,
    Mp4,
    Ogg,
    Wav
}

public sealed class MasteringOutput : MasteringPreviewOutput
{
    [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public MasterOutputKind? Kind { get; set; }
}

public sealed class MasteringJobDescription
{
    [JsonProperty("inputs")]
    public IEnumerable<MasteringInput> Inputs { get; }

    [JsonProperty("outputs")]
    public IEnumerable<MasteringOutput> Outputs { get; }

    [JsonProperty("on_complete")]
    public MasteringOnComplete OnComplete { get; }

    public MasteringJobDescription(MasteringInput input, params MasteringOutput[] outputs)
    {
        Inputs = new[] { input };
        Outputs = outputs;
    }
}

public sealed class MasteringPreviewJobDescription
{
    [JsonProperty("inputs")]
    public IEnumerable<MasteringPreviewInput> Inputs { get; }

    [JsonProperty("outputs")]
    public IEnumerable<MasteringPreviewOutput> Outputs { get; }

    [JsonProperty("on_complete")]
    public MasteringOnComplete OnComplete { get; }

    public MasteringPreviewJobDescription(MasteringPreviewInput input, params MasteringPreviewOutput[] outputs)
    {
        Inputs = new[] { input };
        Outputs = outputs;
    }
}

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
