using Newtonsoft.Json;

namespace DolbyIO.Rest.Media.Models;

public sealed class FileLocationAuth
{
    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
    public string Key { get; }

    [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
    public string Secret { get; }

    [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
    public string Token { get; }

    [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
    public string Username { get; }

    [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
    public string Password { get; }

    /// <summary>
    /// AWS credentials.
    /// </summary>
    /// <param name="key">The AWS client access key.</param>
    /// <param name="secret">The AWS client secret.</param>
    /// <param name="token">The AWS client tokenb.</param>
    public FileLocationAuth(string key, string secret, string? token = null)
    {
        Key = key;
        Secret = secret;
        Token = token;
    }

    /// <summary>
    /// Authentication.
    /// </summary>
    /// <param name="username">The user name or email address used for authentication.</param>
    /// <param name="password">The password or secret used to authenticate.</param>
    public FileLocationAuth(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

public sealed class InputRegion
{
    [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
    public int? Start { get; }

    [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
    public int? End { get; }

    /// <summary>
    /// Specifies the section of the file that the API will process.
    /// </summary>
    /// <param name="start">
    /// Specifies the start position in seconds.
    /// In absence of this, region is identified to be from the beginning of the file to the specified end position.
    /// </param>
    /// <param name="end">
    ///Specifies the end position in seconds.
    /// In absence of this, region is identified to be from the specified start position to the end of file.
    /// </param>
    public InputRegion(int? start = null, int? end = null)
    {
        Start = start;
        End = end;
    }
}

public class InputLocation
{
    [JsonProperty("url")]
    public string Url { get; }

    [JsonProperty("auth", NullValueHandling = NullValueHandling.Ignore)]
    public FileLocationAuth Auth { get; set; }

    public InputLocation(string url)
    {
        Url = url;
    }
}

public sealed class OutputLocation
{
    [JsonProperty("url")]
    public string Url { get; }

    [JsonProperty("auth", NullValueHandling = NullValueHandling.Ignore)]
    public FileLocationAuth Auth { get; set; }

    public OutputLocation(string url)
    {
        Url = url;
    }
}
