using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class PublishTokenStream
{
    [JsonProperty("streamName")]
    public string StreamName { get; internal set; }

    [JsonProperty("isRegex")]
    public bool IsRegex { get; internal set; }
}

public sealed class ReadPublishTokenData
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
    public IEnumerable<PublishTokenStream> Streams { get; internal set; }

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

    [JsonProperty("subscribeRequiresAuth")]
    public bool SubscribeRequiresAuth { get; internal set; }

    [JsonProperty("record")]
    public bool Record { get; internal set; }

    [JsonProperty("multisource")]
    public bool Multisource { get; internal set; }
}

public sealed class ReadPublishTokenResponse : BaseResponse<ReadPublishTokenData>
{
}

public sealed class DeletePublishTokenResponse : BaseResponse<bool>
{
}
