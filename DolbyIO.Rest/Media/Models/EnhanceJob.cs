using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DolbyIO.Rest.Media.Models;

public enum EnhanceContentTypes
{
    Conference,
    Interview,
    Lecture,
    Meeting,
    MobilePhone,
    Music,
    Podcast,
    Studio,
    VoiceOver,
    VoiceRecording,
    Other
}

public sealed class EnhanceContentType
{
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public EnhanceContentTypes Type { get; }

    internal EnhanceContentType(EnhanceContentTypes contentType)
    {
        Type = contentType;
    }
}

public sealed class SpeechIsolation
{
    [JsonProperty("enable")]
    public bool Enable { get; }

    [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
    public int? Amount { get; }

    /// <summary>
    /// Speech isolation is enabled by default as part of the intelligent noise management.
    /// This type of noise refers to any nuisance sounds not attributable to speech or dialog.
    /// </summary>
    /// <param name="enable">Option to disable speech isolation.</param>
    /// <param name="amount">
    /// The amount of speech isolation to apply. The amount refers to how aggressive the processing will be in a range from 0 to 100. By default this will range in the 20-80% range depending on the analysis of your media.
    /// You can set this amount to the extremes if you find content important to the context is dropped or picked up to focus more or less on the speech.
    /// </param>
    public SpeechIsolation(bool enable, int? amount = null)
    {
        Enable = enable;
        Amount = amount;
    }
}

public enum ReductionAmount
{
    Low,
    Medium,
    High,
    Max
}

public sealed class Reduction
{
    [JsonProperty("enable")]
    public bool Enable { get; }

    [JsonProperty("amount")]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public ReductionAmount Amount { get; }

    internal Reduction(bool enable, ReductionAmount amount)
    {
        Enable = enable;
        Amount = amount;
    }
}

public sealed class Sibilance
{
    [JsonProperty("reduction")]
    public Reduction Reduction { get; }

    /// <summary>
    /// Reduce the severity of sibilant over-pronunciation.
    /// </summary>
    /// <param name="enable">Option to disable sibilance reduction.</param>
    /// <param name="amount">
    /// The amount of aggressiveness you want the enhancement to make.
    /// You can dial this up or down depending on your media characteristics.
    /// </param>
    public Sibilance(bool enable, ReductionAmount amount)
    {
        Reduction = new Reduction(enable, amount);
    }
}

public sealed class Plosive
{
    [JsonProperty("reduction")]
    public Reduction Reduction { get; }

    /// <summary>
    /// Reduce the intensity of plosives in speech.
    /// </summary>
    /// <param name="enable">Enable for plosive reduction.</param>
    /// <param name="amount">
    /// The amount of aggressiveness you want the enhancement to make.
    /// You can dial this up or down depending on your media characteristics.
    /// </param>
    public Plosive(bool enable, ReductionAmount amount)
    {
        Reduction = new Reduction(enable, amount);
    }
}

public sealed class Click
{
    [JsonProperty("reduction")]
    public Reduction Reduction { get; }

    /// <summary>
    /// Reduce the severity of clicks in your media. This can make speech recordings sound better.
    /// </summary>
    /// <param name="enable">Controls whether click reduction is enabled.</param>
    /// <param name="amount">Controls how strongly click reduction will be applied to the recorded signal.</param>
    public Click(bool enable, ReductionAmount amount)
    {
        Reduction = new Reduction(enable, amount);
    }
}

public sealed class Speech
{
    [JsonProperty("isolation")]
    public SpeechIsolation Isolation { get; set; }

    [JsonProperty("sibilance")]
    public Sibilance Sibilance { get; set; }

    [JsonProperty("plosive")]
    public Plosive Plosive { get; set; }

    [JsonProperty("click")]
    public Click Click { get; set; }
}

public sealed class MusicDetection
{
    [JsonProperty("enable")]
    public bool Enable { get; }

    internal MusicDetection(bool enable)
    {
        Enable = enable;
    }
}

public sealed class Music
{
    [JsonProperty("detection")]
    public MusicDetection Detection { get; }

    internal Music(bool enable)
    {
        Detection = new MusicDetection(enable);
    }
}

public sealed class EnhanceAudio
{
    [JsonProperty("loudness")]
    public string Loudness { get; set; }

    [JsonProperty("dynamics")]
    public string Dynamics { get; set; }

    [JsonProperty("noise")]
    public string Noise { get; set; }

    [JsonProperty("filter")]
    public string Filter { get; set; }

    [JsonProperty("speech")]
    public Speech Speech { get; set; }

    [JsonProperty("music")]
    public Music Music { get; }

    public EnhanceAudio(bool? musicDetactionEnabled = null)
    {
        if (musicDetactionEnabled.HasValue)
        {
            Music = new Music(musicDetactionEnabled.Value);
        }
    }
}

public sealed class EnhanceInputLocation : InputLocation
{
    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public InputRegion Region { get; set; }

    public EnhanceInputLocation(string url) : base(url)
    {
    }
}

public sealed class EnhanceJobDescription
{
    [JsonProperty("input")]
    public EnhanceInputLocation Input { get; }

    [JsonProperty("output")]
    public OutputLocation Output { get; }

    [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
    public EnhanceContentType ContentType { get; }

    [JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
    public EnhanceAudio Audio { get; }

    public EnhanceJobDescription(EnhanceInputLocation input, OutputLocation output, EnhanceContentTypes? contentType = null, EnhanceAudio audio = null)
    {
        Input = input;
        Output = output;
        if (contentType.HasValue)
        {
            ContentType = new EnhanceContentType(contentType.Value);
        }
        Audio = audio;
    }
}

public sealed class EnhanceJobResult
{
    [JsonProperty("version")]
    public string Version { get; set; }
}

public sealed class EnhanceJob : JobResult<EnhanceJobResult>
{
}
