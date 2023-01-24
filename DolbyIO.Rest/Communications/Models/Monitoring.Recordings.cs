using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models;

public sealed class GetRecordingsOptions : PagedOptions
{
}

public sealed class GetAllRecordingsOptions : AllElementsOptions
{
}

public sealed class GetRecordingOptions : PagedOptions
{
    /// <summary>
    /// Gets or sets the identifier of the conference.
    /// </summary>
    public string ConferenceId { get; set; }
}

public sealed class Recording
{
    /// <summary>
    /// Gets or sets the ID for the conference.
    /// </summary>
    [JsonProperty("confId")]
    public string ConferenceId { get; set; }

    /// <summary>
    /// Gets or sets the alias of the conference.
    /// </summary>
    [JsonProperty("alias")]
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration of the recording.
    /// </summary>
    [JsonProperty("duration")]
    public long Duration { get; set; }

    /// <summary>
    /// Gets or sets the epoch time of the end of the recording.
    /// </summary>
    [JsonProperty("ts")]
    public long Ts { get; set; }

    /// <summary>
    /// Gets or sets the region code in which the recording took place.
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the list of video or non-Dolby Voice audio mix recordings.
    /// </summary>
    [JsonProperty("mix")]
    public MixRecording Mix { get; set; }

    /// <summary>
    /// Gets or sets the list of Dolby Voice-based audio recordings.
    /// </summary>
    [JsonProperty("audio")]
    public AudioRecording Audio { get; set; }
}

public sealed class MixRecording
{
    /// <summary>
    /// Gets or sets the size of the MP4 recording (in bytes).
    /// The MP4 recording is available only if you have requested the MP4 format in the recording settings.
    /// For more information, see the <seealso cref="https://docs.dolby.io/communications-apis/docs/guides-recording-mechanisms">Recording</seealso> document.
    /// </summary>
    [JsonProperty("mp4")]
    public long Mp4 { get; set; }

    /// <summary>
    /// Gets or sets the size of the MP3 recording (in bytes).
    /// The MP3 recording is available only if you have requested the MP3 format in the recording settings.
    /// For more information, see the <seealso cref="https://docs.dolby.io/communications-apis/docs/guides-recording-mechanisms">Recording</seealso> document.
    /// </summary>
    [JsonProperty("mp3")]
    public long Mp3 { get; set; }

    /// <summary>
    /// Gets or sets the region code in which the mix recording took place.
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }
}

public sealed class RecordingFile
{
    /// <summary>
    /// Gets or sets the time when the conference started, in milliseconds since epoch.
    /// </summary>
    [JsonProperty("startTime")]
    public long StartTime { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration of the recording.
    /// </summary>
    [JsonProperty("duration")]
    public long Duration { get; set; }

    /// <summary>
    /// Gets or sets the size of the recording (in bytes).
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the unique name of the recording file.
    /// </summary>
    [JsonProperty("fileName")]
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the presigned URL, with limited validity, where you can download the recording file with a GET.
    /// The URL only applies when accessing specific audio recording data.
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the list of split audio recordings.
    /// </summary>
    [JsonProperty("splits")]
    public IEnumerable<object> Splits { get; set; }
}

public sealed class AudioRecording
{
    /// <summary>
    /// Gets or sets the region code in which the mix recording took place.
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the list of audio recordings.
    /// </summary>
    [JsonProperty("records")]
    public IEnumerable<RecordingFile> Records { get; set; }
}

public sealed class GetRecordingsResponse : PagedResponse
{
    /// <summary>
    /// Gets or sets the list of recordings.
    /// </summary>
    [JsonProperty("recordings")]
    public IEnumerable<Recording> Recordings { get; set; }
}

public sealed class ConferenceDetails
{
    /// <summary>
    /// Gets or sets the conference identifier.
    /// </summary>
    [JsonProperty("confId")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the conference alias, provided by the customer.
    /// </summary>
    [JsonProperty("confAlias")]
    public string Alias { get; set; }
}

public sealed class DolbyVoiceRecording
{
    /// <summary>
    /// Gets or sets the region code in which the mix recording took place.
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the conference details.
    /// </summary>
    [JsonProperty("conference")]
    public ConferenceDetails Conference { get; set; }

    /// <summary>
    /// Gets or sets the list of audio recordings.
    /// </summary>
    [JsonProperty("records")]
    public IEnumerable<RecordingFile> Records { get; set; }
}
