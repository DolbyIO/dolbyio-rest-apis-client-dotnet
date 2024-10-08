using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class PublishTokens
{
    private const string URL_BASE = Urls.STREAMING_BASE_URL + "/api/publish_token";

    private readonly HttpClient _httpClient;

    internal PublishTokens(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PublishToken> ReadAsync(string apiSecret, int tokenId)
    {
        string url = $"{URL_BASE}/{tokenId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<PublishToken>(request);
    }

    public async Task<bool> DeleteAsync(string apiSecret, int tokenId)
    {
        string url = $"{URL_BASE}/{tokenId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Delete, url, apiSecret);
        return await _httpClient.GetResponseAsync<bool>(request);
    }

    public async Task<PublishToken> UpdateAsync(string apiSecret, int tokenId, UpdatePublishTokenData update)
    {
        string url = $"{URL_BASE}/{tokenId}";
        string content = JsonConvert.SerializeObject(update);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Put, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<PublishToken>(request);
    }

    public enum ListSortBy
    {
        Name,
        AddedOn,
        None
    }

    public async Task<IEnumerable<PublishToken>> ListAsync(string apiSecret, int page, int itemsOnPage, ListSortBy sortBy = ListSortBy.None, bool isDescending = false)
    {
        var uriBuilder = new UriBuilder(Urls.STREAMING_BASE_URL);
        uriBuilder.Path = "/api/publish_token/list";

        var nvc = new NameValueCollection();
        nvc.Add("page", page.ToString());
        nvc.Add("itemsOnPage", itemsOnPage.ToString());
        nvc.Add("isDescending", isDescending.ToString());
        if (sortBy != ListSortBy.None)
        {
            nvc.Add("sortBy", Enum.GetName(typeof(ListSortBy), sortBy));
        }

        uriBuilder.Query = nvc.ToString();

        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, uriBuilder.ToString(), apiSecret);
        return await _httpClient.GetResponseAsync<IEnumerable<PublishToken>>(request);
    }

    public async Task<PublishToken> CreateAsync(string apiSecret, CreatePublishToken create)
    {
        string content = JsonConvert.SerializeObject(create);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, URL_BASE, apiSecret, content);
        return await _httpClient.GetResponseAsync<PublishToken>(request);
    }

    public async Task<ActivePublishToken> GetActiveTokensAsync(string apiSecret, string streamId)
    {
        string url = $"{URL_BASE}/active?streamId={streamId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<ActivePublishToken>(request);
    }

    public async Task<ActivePublishToken> GetAllActiveTokensAsync(string apiSecret)
    {
        const string url = URL_BASE + "/active/all";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<ActivePublishToken>(request);
    }

    public async Task<DisablePublishTokenResponse> DisableAsync(string apiSecret, IEnumerable<int> tokenIds)
    {
        const string url = URL_BASE + "/disable";
        var body = new
        {
            tokenIds = tokenIds
        };
        string content = JsonConvert.SerializeObject(body);

        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Patch, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<DisablePublishTokenResponse>(request);
    }
}
