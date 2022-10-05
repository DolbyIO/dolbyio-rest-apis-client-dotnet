using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models;

public sealed class RemixStatus
{
    /// <summary>
    /// Gets or sets the status of the current remix job.
    /// </summary>
    /// <value>
    /// The possible values are:
    /// <list type="bullet">
    ///     <item>
    ///         <term>UNKNOWN</term>
    ///     </item>
    ///     <item>
    ///         <term>ERROR</term>
    ///     </item>
    ///     <item>
    ///         <term>IN_PROGRESS</term>
    ///     </item>
    ///     <item>
    ///         <term>COMPLETED</term>
    ///     </item>
    /// </list>
    /// </value>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the two-letter code identifying the region where the conference is hosted.
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the conference alias, provided by the customer as a way to identify one or more conferences.
    /// </summary>
    [JsonProperty("alias")]
    public string Alias { get; set; }
}
