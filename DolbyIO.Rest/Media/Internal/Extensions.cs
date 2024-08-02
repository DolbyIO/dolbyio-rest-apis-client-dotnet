using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using DolbyIO.Rest.Media.Models;

namespace DolbyIO.Rest.Media;

internal static class Extensions
{
    public static async Task<string> StartJobAsync(this HttpClient httpClient, JwtToken accessToken, string urlPath, string jobDescription)
    {
        string url = $"{Urls.MAPI_BASE_URL}{urlPath}";
        JobCreationResult result = await httpClient.SendPostAsync<JobCreationResult>(url, accessToken, jobDescription);
        return result.JobId;
    }

    public static async Task<TResult> GetJobResultAsync<TResult>(this HttpClient httpClient, JwtToken accessToken, string urlPath, string jobId)
        where TResult: class
    {
        var uriBuilder = new UriBuilder(Urls.MAPI_BASE_URL);
        uriBuilder.Path = urlPath;

        var nvc = new NameValueCollection();
        nvc.Add("job_id", jobId);
        uriBuilder.Query = nvc.ToString();

        return await httpClient.SendGetAsync<TResult>(uriBuilder.ToString(), accessToken);
    }
}
