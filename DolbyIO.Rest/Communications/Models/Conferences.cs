using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models;

public sealed class CreateConferenceOptions
{
    /// <summary>
    /// Gets or sets the conference owner's external ID.
    /// </summary>
    public string OwnerExternalId { get; set; }

    /// <summary>
    /// Gets or sets the conference alias, provided by the customer as a way to identify one or more conferences.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets the PIN code of the conference. This applies to conferences using PSTN (telephony network).
    /// </summary>
    public string PinCode { get; set; }

    /// <summary>
    /// Gets or sets if Dolby Voice is enabled for the conference. The <c>true</c> value creates the conference with Dolby Voice enabled.
    /// </summary>
    public bool? DolbyVoice { get; set; }

    /// <summary>
    /// Gets or sets if live recording is enabled for the conference. When set to <c>true</c>, the recorded file is available at the end of the call and can be downloaded immediately.
    /// </summary>
    public bool? LiveRecording { get; set; }

    /// <summary>
    /// Gets or sets the bitrate adaptation mode for the video transmission.
    /// </summary>
    public RTCPMode RTCPMode { get; set; }

    /// <summary>
    /// Gets or sets the time to live that enables customizing the waiting time (in seconds) and terminating empty conferences.
    /// </summary>
    public int? Ttl { get; set; }

    /// <summary>
    /// Gets or sets the video codec (VP8 or H264) for the conference.
    /// </summary>
    public VideoCodec VideoCodec { get; set; }

    /// <summary>
    /// Gets or sets if the conference does not allow participants to enable video.
    /// </summary>
    public bool? AudioOnly { get; set; }

    /// <summary>
    /// Gets or sets the list of participants.
    /// </summary>
    public IEnumerable<Participant> Participants { get; set; }

    /// <summary>
    /// Gets or sets the recording format. Valid values are <c>mp3</c> and <c>mp4</c>.
    /// </summary>
    public IEnumerable<string> RecordingFormats { get; set; }
}

public enum RTCPMode
{
    /// <summary>
    /// Adjusts the transmission bitrate to the receiver who has the worst network conditions.
    /// </summary>
    Worst,
    /// <summary>
    /// Averages the available bandwidth of all the receivers and adjusts the transmission bitrate to this value.
    /// </summary>
    Average,
    /// <summary>
    /// Does not adjust the transmission bitrate to the receiver’s bandwidth.
    /// </summary>
    Max
}

public enum VideoCodec
{
    VP8,
    H264
}

public sealed class Participant
{
    public string ExternalId { get; set; }

    public IEnumerable<Permission> Permissions { get; set; }

    public bool Notify { get; set; }
}

public enum Permission
{
    /// <summary>
    /// Allows a participant to invite participants to a conference.
    /// </summary>
    INVITE,

    /// <summary>
    /// Allows a participant to join a conference.
    /// </summary>
    JOIN,

    /// <summary>
    /// Allows a participant to send an audio stream during a conference.
    /// </summary>
    SEND_AUDIO,

    /// <summary>
    /// Allows a participant to send a video stream during a conference.
    /// </summary>
    SEND_VIDEO,
    
    /// <summary>
    /// Allows a participant to share their screen during a conference.
    /// </summary>
    SHARE_SCREEN,
    
    /// <summary>
    /// Allows a participant to share a video during a conference.
    /// </summary>
    SHARE_VIDEO,
    
    /// <summary>
    /// Allows a participant to share a file during a conference.
    /// </summary>
    SHARE_FILE,
    
    /// <summary>
    /// Allows a participant to send a message to other participants during a conference.
    /// </summary>
    SEND_MESSAGE,
    
    /// <summary>
    /// Allows a participant to record a conference.
    /// </summary>
    RECORD,
    
    /// <summary>
    /// Allows a participant to stream a conference.
    /// </summary>
    STREAM,
    
    /// <summary>
    /// Allows a participant to kick other participants from a conference.
    /// </summary>
    KICK,
    
    /// <summary>
    /// Allows a participant to update other participants' permissions.
    /// </summary>
    UPDATE_PERMISSIONS,
}

internal sealed class CreateConferenceRequest
{
    [JsonProperty("alias")]
    public string Alias { get; set; }

    [JsonProperty("ownerExternalId")]
    public string OwnerExternalId { get; set; }

    [JsonProperty("parameters")]
    public CreateConferenceParametersRequest Parameters { get; set; }

    [JsonProperty("participants")]
    public IDictionary<string, CreateConferenceParticipantRequest> Participants { get; set;  }
}

internal sealed class CreateConferenceParametersRequest
{
    [JsonProperty("pinCode")]
    public string PinCode { get; set; }

    [JsonProperty("dolbyVoice")]
    public bool? DolbyVoice { get; set; }

    [JsonProperty("liveRecording")]
    public bool? LiveRecording { get; set; }

    [JsonProperty("rtcpMode")]
    public string RTCPMode { get; set; }

    [JsonProperty("ttl")]
    public int? Ttl { get; set; }

    [JsonProperty("videoCodec")]
    public string VideoCodec { get; set; }

    [JsonProperty("audioOnly")]
    public bool? AudioOnly { get; set; }

    [JsonProperty("recording")]
    public IEnumerable<string> RecordingFormats { get; set; }
}

internal sealed class CreateConferenceParticipantRequest
{
    [JsonProperty("permissions")]
    public IEnumerable<string> Permissions { get; set; }

    [JsonProperty("notification")]
    public bool? Notification { get; set; }
}

public sealed class Conference
{
    [JsonProperty("conferenceId")]
    public string Id { get; set; }

    [JsonProperty("conferenceAlias")]
    public string Alias { get; set; }

    [JsonProperty("conferencePincode")]
    public string Pincode { get; set; }

    [JsonProperty("isProtected")]
    public string IsProtected { get; set; }

    [JsonProperty("ownerToken")]
    public string OwnerToken { get; set; }
}

public sealed class CartesianCoordinates
{
    [JsonProperty("x")]
    public float X { get; set; }

    [JsonProperty("y")]
    public float Y { get; set; }

    [JsonProperty("z")]
    public float Z { get; set; }
}

public sealed class SpatialEnvironment
{
    [JsonProperty("scale")]
    public CartesianCoordinates Scale { get; set; }

    [JsonProperty("forward")]
    public CartesianCoordinates Forward { get; set; }

    [JsonProperty("up")]
    public CartesianCoordinates Up { get; set; }

    [JsonProperty("right")]
    public CartesianCoordinates Right { get; set; }
}

public sealed class SpatialListener
{
    [JsonProperty("position")]
    public CartesianCoordinates Position { get; set; }

    [JsonProperty("direction")]
    public CartesianCoordinates Direction { get; set; }
}
