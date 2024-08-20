using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class SubscribeTokenTracking
{
    [JsonProperty("trackingId")]
    public string TrackingId { get; set; }
}

public sealed class SubscribeTokenStream
{
    [JsonProperty("streamName")]
    public string StreamName { get; set; }

    [JsonProperty("isRegex")]
    public bool IsRegex { get; set; }
}

public sealed class SubscribeToken
{
    [JsonProperty("id")]
    public int Id { get; internal set; }

    [JsonProperty("label")]
    public string Label { get; internal set; }

    [JsonProperty("token")]
    public string Token { get; internal set; }

    [JsonProperty("addedOn")]
    public DateTime AddedOn { get; internal set; }

    [JsonProperty("expiresOn")]
    public DateTime ExpiresOn { get; internal set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; internal set; }

    [JsonProperty("streams")]
    public IEnumerable<SubscribeTokenStream> Streams { get; internal set; }

    [JsonProperty("allowedOrigins")]
    public IEnumerable<string> AllowedOrigins { get; internal set; }

    [JsonProperty("allowedIpAddresses")]
    public IEnumerable<string> AllowedIpAddresses { get; internal set; }

    [JsonProperty("bindIpsOnUsage")]
    public int BindIpsOnUsage { get; internal set; }

    [JsonProperty("allowedCountries")]
    public IEnumerable<string> AllowedCountries { get; internal set; }

    [JsonProperty("deniedCountries")]
    public IEnumerable<string> DeniedCountries { get; internal set; }

    [JsonProperty("originCluster")]
    public string OriginCluster { get; internal set; }

    [JsonProperty("tracking")]
    public SubscribeTokenTracking Tracking { get; internal set; }
}

public sealed class UpdateSubscribeToken
{
    [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
    public string Label { get; set; }

    [JsonProperty("refreshToken", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RefreshToken { get; set; }

    [JsonProperty("isActive", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsActive { get; set; }

    [JsonProperty("addTokenStreams", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<SubscribeTokenStream> AddTokenStreams { get; set; }

    [JsonProperty("removeTokenStreams", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> RemoveTokenStreams { get; set; }

    [JsonProperty("updateAllowedOrigins", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateAllowedOrigins { get; set; }

    [JsonProperty("updateAllowedIpAddresses", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateAllowedIpAddresses { get; set; }

    [JsonProperty("updateBindIpsOnUsage", NullValueHandling = NullValueHandling.Ignore)]
    public int? UpdateBindIpsOnUsage { get; set; }

    [JsonProperty("updateAllowedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateAllowedCountries { get; set; }

    [JsonProperty("updateDeniedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateDeniedCountries { get; set; }

    [JsonProperty("updateOriginCluster", NullValueHandling = NullValueHandling.Ignore)]
    public string UpdateOriginCluster { get; set; }
}

public sealed class CreateSubscribeToken
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("expires", NullValueHandling = NullValueHandling.Ignore)]
    public int? Expires { get; set; }

    [JsonProperty("streams", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<PublishTokenStream> Streams { get; set; }

    [JsonProperty("allowedOrigins", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> AllowedOrigins { get; set; }

    [JsonProperty("allowedIpAddresses", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> AllowedIpAddresses { get; set; }

    [JsonProperty("bindIpsOnUsage", NullValueHandling = NullValueHandling.Ignore)]
    public int? BindIpsOnUsage { get; set; }

    [JsonProperty("allowedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> AllowedCountries { get; set; }

    [JsonProperty("deniedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> DeniedCountries { get; set; }

    [JsonProperty("originCluster", NullValueHandling = NullValueHandling.Ignore)]
    public string OriginCluster { get; set; }

    [JsonProperty("tracking", NullValueHandling = NullValueHandling.Ignore)]
    public SubscribeTokenTracking Tracking { get; set; }
}
