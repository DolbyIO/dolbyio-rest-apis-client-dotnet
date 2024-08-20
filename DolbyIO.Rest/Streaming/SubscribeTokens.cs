using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming;

public sealed class SubscribeTokens
{
    private const string URL_BASE = Urls.STREAMING_BASE_URL + "/api/subscribe_token";

    private readonly HttpClient _httpClient;

    internal SubscribeTokens(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SubscribeToken> ReadAsync(string apiSecret, int tokenId)
    {
        string url = $"{URL_BASE}/{tokenId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Get, url, apiSecret);
        return await _httpClient.GetResponseAsync<SubscribeToken>(request);
    }

    public async Task<bool> DeleteAsync(string apiSecret, int tokenId)
    {
        string url = $"{URL_BASE}/{tokenId}";
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessageBase(HttpMethod.Delete, url, apiSecret);
        return await _httpClient.GetResponseAsync<bool>(request);
    }

    public async Task<SubscribeToken> UpdateAsync(string apiSecret, int tokenId, UpdateSubscribeToken update)
    {
        string url = $"{URL_BASE}/{tokenId}";
        string content = JsonConvert.SerializeObject(update);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Put, url, apiSecret, content);
        return await _httpClient.GetResponseAsync<SubscribeToken>(request);
    }

    public enum ListSortBy
    {
        Name,
        AddedOn,
        None
    }

    public async Task<IEnumerable<SubscribeToken>> ListAsync(string apiSecret, int page, int itemsOnPage, ListSortBy sortBy = ListSortBy.None, bool isDescending = false)
    {
        var uriBuilder = new UriBuilder(Urls.STREAMING_BASE_URL);
        uriBuilder.Path = "/api/subscribe_token/list";

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
        return await _httpClient.GetResponseAsync<IEnumerable<SubscribeToken>>(request);
    }

    public async Task<SubscribeToken> CreateAsync(string apiSecret, CreateSubscribeToken create)
    {
        string content = JsonConvert.SerializeObject(create);
        using HttpRequestMessage request = Extensions.BuildHttpRequestMessage(HttpMethod.Post, URL_BASE, apiSecret, content);
        return await _httpClient.GetResponseAsync<SubscribeToken>(request);
    }
}
