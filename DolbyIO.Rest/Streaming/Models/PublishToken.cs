using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class PublishTokenStream
{
    [JsonProperty("streamName")]
    public string StreamName { get; set; }

    [JsonProperty("isRegex")]
    public bool IsRegex { get; set; }
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

public sealed class ListPublishTokenResponse : BaseResponse<ReadPublishTokenData[]>
{
}

public sealed class UpdatePublishTokenData
{
    [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
    public string Label { get; set; }

    [JsonProperty("refreshToken", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RefreshToken { get; set; }

    [JsonProperty("isActive", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsActive { get; set; }

    [JsonProperty("addTokenStreams", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<PublishTokenStream> AddTokenStreams { get; set; }

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

    [JsonProperty("subscribeRequiresAuth", NullValueHandling = NullValueHandling.Ignore)]
    public bool? SubscribeRequiresAuth { get; set; }

    [JsonProperty("record", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Record { get; set; }

    [JsonProperty("multisource", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Multisource { get; set; }
}

public sealed class CreatePublishTokenData
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

    [JsonProperty("subscribeRequiresAuth", NullValueHandling = NullValueHandling.Ignore)]
    public bool? SubscribeRequiresAuth { get; set; }

    [JsonProperty("record", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Record { get; set; }

    [JsonProperty("multisource", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Multisource { get; set; }
}

public sealed class DeletePublishTokenResponse : BaseResponse<bool>
{
}

public sealed class GetActivePublishTokenData
{
    [JsonProperty("tokenIds")]
    public int[] TokenIds { get; internal set; }
}

public sealed class GetActivePublishTokenResponse : BaseResponse<GetActivePublishTokenData>
{
}

public sealed class FailedToken
{
    [JsonProperty("tokenId")]
    public int TokenId { get; internal set; }

    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; internal set; }
}

public sealed class DisablePublishTokenData
{
    [JsonProperty("successfulTokens")]
    public int[] SuccessfulTokens { get; internal set; }

    [JsonProperty("failedTokens")]
    public FailedToken[] FailedTokens { get; internal set; }
}

public sealed class DisablePublishTokenResponse : BaseResponse<DisablePublishTokenData>
{
}
