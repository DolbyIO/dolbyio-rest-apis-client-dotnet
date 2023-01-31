using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class PublishToken
{
    private const string URL_BASE = Urls.SAPI_BASE_URL + "/api/publish_token";

    private readonly HttpClient _httpClient;

    internal PublishToken(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReadPublishTokenResponse> ReadAsync(string apiSecret, int tokenId)
    {
        string url = $"{URL_BASE}/{tokenId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<ReadPublishTokenResponse>(request);
    }

    public async Task<DeletePublishTokenResponse> DeleteAsync(string apiSecret, int tokenId)
    {
        string url = $"{URL_BASE}/{tokenId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Delete, url, apiSecret);
        return await _httpClient.GetResponseAsync<DeletePublishTokenResponse>(request);
    }

    public async Task<ReadPublishTokenResponse> UpdateAsync(string apiSecret, int tokenId, UpdatePublishTokenData update)
    {
        string url = $"{URL_BASE}/{tokenId}";
        string content = JsonConvert.SerializeObject(update);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Put, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<ReadPublishTokenResponse>(request);
    }

    public enum ListSortBy
    {
        Name,
        AddedOn,
        None
    }

    public async Task<ListPublishTokenResponse> ListAsync(string apiSecret, int page, int itemsOnPage, ListSortBy sortBy = ListSortBy.None, bool isDescending = false)
    {
        var uriBuilder = new UriBuilder(Urls.SAPI_BASE_URL);
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
        return await _httpClient.GetResponseAsync<ListPublishTokenResponse>(request);
    }

    public async Task<ReadPublishTokenResponse> CreateAsync(string apiSecret, CreatePublishTokenData create)
    {
        string content = JsonConvert.SerializeObject(create);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, URL_BASE, apiSecret, content);
        return await _httpClient.GetResponseAsync<ReadPublishTokenResponse>(request);
    }

    public async Task<GetActivePublishTokenResponse> GetActiveTokensAsync(string apiSecret, string streamId)
    {
        string url = $"{URL_BASE}/active?streamId={streamId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<GetActivePublishTokenResponse>(request);
    }

    public async Task<GetActivePublishTokenResponse> GetAllActiveTokensAsync(string apiSecret)
    {
        const string url = URL_BASE + "/active/all";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<GetActivePublishTokenResponse>(request);
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
