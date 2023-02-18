using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Communications.Models;
using DolbyIO.Rest.Models;

namespace DolbyIO.Rest.Communications;

public sealed class Conferences
{
    private readonly HttpClient _httpClient;

    internal Conferences(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Creates a conference.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/api-recording-start"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="options">Options to create the conference.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the newly created <see cref="Conference" />.</returns>
    public async Task<Conference> CreateAsync(JwtToken accessToken, CreateConferenceOptions options)
    {
        var requestObject = new CreateConferenceRequest
        {
            Alias = options.Alias,
            OwnerExternalId = options.OwnerExternalId,
            Parameters = new CreateConferenceParametersRequest
            {
                PinCode = options.PinCode,
                DolbyVoice = options.DolbyVoice,
                LiveRecording = options.LiveRecording,
                RTCPMode = Enum.GetName(typeof(RTCPMode), options.RTCPMode).ToLowerInvariant(),
                Ttl = options.Ttl,
                VideoCodec = Enum.GetName(typeof(VideoCodec), options.VideoCodec),
                AudioOnly = options.AudioOnly,
                RecordingFormats = options.RecordingFormats,
            },
            Participants = options.Participants?.ToDictionary(p => p.ExternalId, p => new CreateConferenceParticipantRequest
            {
                Permissions = p.Permissions?.Select(pe => Enum.GetName(typeof(Permission), pe)),
                Notification = p.Notify
            })
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/create";
        if (!string.IsNullOrWhiteSpace(options.Region))
            url = url.Replace("https://", $"https://{options.Region}.");

        return await _httpClient.SendPostAsync<CreateConferenceRequest, Conference>(url, accessToken, requestObject);
    }

    /// <summary>
    /// Invites participants to an ongoing conference.
    /// This API can also be used to generate new conference access tokens for an ongoing conference.
    /// If the invite request includes participants that are already in the conference,
    /// a new conference access token is not generated and an invitation is not sent.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/invite-to-conference"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="participants">List of participants to invite to the conference.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of user tokens for each participants.</returns>
    public async Task<IDictionary<string, string>> InviteAsync(JwtToken accessToken, string conferenceId, IEnumerable<Participant> participants)
    {
        var body = new
        {
            participants = participants.ToDictionary(p => p.ExternalId, p => new CreateConferenceParticipantRequest
            {
                Permissions = p.Permissions?.Select(pe => Enum.GetName(typeof(Permission), pe)),
                Notification = p.Notify
            })
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/{conferenceId}/invite";
        return await _httpClient.SendPostAsync<dynamic, Dictionary<string, string>>(url, accessToken, body);
    }

    /// <summary>
    /// Kicks participants from an ongoing conference.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/kick-from-conference"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="externalIds">External identifiers of the participants to kick our of the conference.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task KickAsync(JwtToken accessToken, string conferenceId, IEnumerable<string> externalIds)
    {
        var body = new { externalIds = externalIds };
        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/{conferenceId}/kick";
        await _httpClient.SendPostAsync(url, accessToken, body);
    }

    /// <summary>
    /// Sends a message to some or all participants in a conference.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/kick-from-conference"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="fromExternalId">The external ID of the author of the message.</param>
    /// <param name="toExternalIds">A list of external IDs that will receive the message. If empty, the message will be broadcasted to all participants in the conference.</param>
    /// <param name="message">The message to send.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task SendMessageAsync(JwtToken accessToken, string conferenceId, string fromExternalId, IEnumerable<string> toExternalIds, string message)
    {
        var body = new {
            from = fromExternalId,
            message = message,
            to = toExternalIds
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/{conferenceId}/message";
        await _httpClient.SendPostAsync(url, accessToken, body);
    }

    /// <summary>
    /// Sets the spatial audio scene for all listeners in an ongoing conference.
    /// This sets the spatial audio environment, the position and direction for all listeners with the spatialAudio flag enabled.
    /// The calls are not cumulative, and each call sets all the spatial listener values.
    /// Participants who do not have a position set are muted.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="environment">The spatial environment of an application, so the audio renderer understands which directions the application considers forward, up, and right and which units it uses for distance.</param>
    /// <param name="listener">The listener's audio position and direction, defined using Cartesian coordinates.</param>
    /// <param name="users">The users' audio positions, defined using Cartesian coordinates.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task SetSpatialListenersAudioAsync(JwtToken accessToken, string conferenceId, SpatialEnvironment environment, SpatialListener listener, IDictionary<string, CartesianCoordinates> users)
    {
        var body = new
        {
            environment = environment,
            listener = listener,
            users = users
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/{conferenceId}/spatial-listeners-audio";
        await _httpClient.SendPutAsync(url, accessToken, body);
    }

    /// <summary>
    /// Update permissions for participants in a conference. When a participant's permissions are updated, the new token is sent directly to the SDK.
    /// The SDK automatically receives, stores, and manages the new token and a <c>permissionsUpdated</c> event is sent.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/update-permissions"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <param name="participants">List of the participants with their new permissions.</param>
    /// <returns>The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the list of user tokens for each participants.</returns>
    public async Task<IDictionary<string, string>> UpdatePermissionsAsync(JwtToken accessToken, string conferenceId, IEnumerable<Participant> participants)
    {
        var body = new
        {
            participants = participants.ToDictionary(p => p.ExternalId, p => new CreateConferenceParticipantRequest
            {
                Permissions = p.Permissions?.Select(pe => Enum.GetName(typeof(Permission), pe)),
                Notification = p.Notify
            })
        };

        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/{conferenceId}/permissions";
        return await _httpClient.SendPostAsync<dynamic, Dictionary<string, string>>(url, accessToken, body);
    }

    /// <summary>
    /// Terminates an ongoing conference and removes all remaining participants from the conference.<br/>
    /// See: <seealso cref="https://docs.dolby.io/communications-apis/reference/terminate-conference"/>
    /// </summary>
    /// <param name="accessToken">Access token to use for authentication.</param>
    /// <param name="conferenceId">Identifier of the conference.</param>
    /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
    public async Task TerminateAsync(JwtToken accessToken, string conferenceId)
    {
        string url = $"{Urls.CAPI_BASE_URL}/v2/conferences/{conferenceId}";
        await _httpClient.SendDeleteAsync(url, accessToken);
    }
}
