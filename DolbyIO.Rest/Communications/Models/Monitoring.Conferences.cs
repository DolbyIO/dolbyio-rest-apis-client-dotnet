using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models;

public sealed class ListConferencesOptions : PagedOptions
{
    /// <summary>
    /// Search conferences using Alias. Use regular expression to search for conferences with similar aliases. For example:
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>foobar</c></term>
    ///         <description>gets all conferences with alias <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.*foobar</c></term>
    ///         <description>gets all conferences with alias ending with <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>foobar.*</c></term>
    ///         <description>gets all conferences with alias starting with <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.*foobar.*</c></term>
    ///         <description>gets all conferences with alias containing <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.*2021.*|.*2022.*</c></term>
    ///         <description>gets all conferences with alias containing either 2021 or 2022.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Search for ongoing references (<c>true</c>) or all conferences (<c>false</c>).
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets the external ID of the participant who created the conference.
    /// </summary>
    public string ExternalId { get; set; }

    /// <summary>
    /// For live conferences, the number of <c>user</c>, <c>listener</c>, and <c>pstn</c> participants.
    /// </summary>
    public bool LiveStats { get; set; }
}

public sealed class ListAllConferencesOptions : AllElementsOptions
{
    /// <summary>
    /// Search conferences using Alias. Use regular expression to search for conferences with similar aliases. For example:
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>foobar</c></term>
    ///         <description>gets all conferences with alias <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.*foobar</c></term>
    ///         <description>gets all conferences with alias ending with <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>foobar.*</c></term>
    ///         <description>gets all conferences with alias starting with <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.*foobar.*</c></term>
    ///         <description>gets all conferences with alias containing <c>foobar</c>.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.*2021.*|.*2022.*</c></term>
    ///         <description>gets all conferences with alias containing either 2021 or 2022.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Search for ongoing references (<c>true</c>) or all conferences (<c>false</c>).
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets the external ID of the participant who created the conference.
    /// </summary>
    public string ExternalId { get; set; }

    /// <summary>
    /// For live conferences, the number of <c>user</c>, <c>listener</c>, and <c>pstn</c> participants.
    /// </summary>
    public bool LiveStats { get; set; }
}

public sealed class ConferenceInfo
{
    [JsonProperty("confId")]
    public string Id { get; set; }

    [JsonProperty("alias")]
    public string Alias { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("dolbyVoice")]
    public bool DolbyVoice { get; set; }

    [JsonProperty("start")]
    public long Start { get; set; }

    [JsonProperty("live")]
    public bool Live { get; set; }

    [JsonProperty("end")]
    public long End { get; set; }

    [JsonProperty("duration")]
    public long Duration { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("presenceDuration")]
    public long PresenceDuration { get; set; }

    [JsonProperty("recordingDuration")]
    public long RecordingDuration { get; set; }

    [JsonProperty("mixerLiveRecording")]
    public long MixerLiveRecording { get; set; }

    [JsonProperty("mixerHlsStreaming")]
    public long MixerHlsStreaming { get; set; }

    [JsonProperty("mixerRtmpStreaming")]
    public long MixerRtmpStreaming { get; set; }

    [JsonProperty("nbUsers")]
    public int NbUsers { get; set; }

    [JsonProperty("nbListeners")]
    public int NbListeners { get; set; }

    [JsonProperty("nbPstn")]
    public int bPstn { get; set; }

    [JsonProperty("owner")]
    public ConferenceOwner Owner { get; set; }

    [JsonProperty("statistics")]
    public ConferenceStatistics Statistics { get; set; }
}

public sealed class ConferenceOwner
{
    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("metadata")]
    public ParticipantMetadata Metadata { get; set; }
}

public sealed class ParticipantMetadata
{
    [JsonProperty("externalName")]
    public string ExternalName { get; set; }

    [JsonProperty("externalId")]
    public string ExternalId { get; set; }

    [JsonProperty("externalPhotoUrl")]
    public string ExternalPhotoUrl { get; set; }

    [JsonProperty("ipAddress")]
    public string IpAddress { get; set; }
}

public sealed class MaxParticipants
{
    [JsonProperty("USER")]
    public int User { get; set; }

    [JsonProperty("LISTENER")]
    public int Listener { get; set; }

    [JsonProperty("MIXER")]
    public int Mixer { get; set; }

    [JsonProperty("PSTN")]
    public int Pstn { get; set; }
}

public sealed class ConferenceStatistics
{
    [JsonProperty("maxParticipants")]
    public MaxParticipants MaxParticipants { get; set; }

    [JsonProperty("network")]
    public object Network { get; set; }
}

public sealed class ListConferencesResponse : PagedResponse
{
    /// <summary>
    /// Gets or sets the list of conferences.
    /// </summary>
    [JsonProperty("conferences")]
    public IEnumerable<ConferenceInfo> Conferences { get; set; }
}

public sealed class GetConferenceParticipantsOptions : PagedOptions
{
    public string ConferenceId { get; set; }

    public string UserId { get; set; }

    public string Type { get; set; }
}

public sealed class GetAllConferenceParticipantsOptions : AllElementsOptions
{
    public string ConferenceId { get; set; }

    public string UserId { get; set; }

    public string Type { get; set; }
}

public sealed class ConferenceParticipant
{
    [JsonProperty("connections")]
    public IEnumerable<object> Connections { get; set; }

    [JsonProperty("stats")]
    public object Stats { get; set; }
}

public sealed class GetConferenceParticipantsResponse : PagedResponse
{
    /// <summary>
    /// Gets or sets the list of participants.
    /// </summary>
    [JsonProperty("participants")]
    public Dictionary<string, ConferenceParticipant> Participants { get; set; }
}
