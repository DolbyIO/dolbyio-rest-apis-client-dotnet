using System.Text;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace DolbyIO.Rest.Tests;

[Collection("SDK")]
public class AuthenticationTests
{
    [Fact]
    public async Task Test_Authentication_GetApiAccessTokenAsync()
    {
        var jwtToken = new JwtToken
        {
            AccessToken = "abcdef",
            TokenType = "Bearer",
            ExpiresIn = 123,
            Scope = "comms:client_access_token:create"
        };

        const string appKey = "app_key";
        const string appSecret = "app_secret";
        string authz = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{appKey}:{appSecret}"));

        var messageHandler = new MockHttpMessageHandler();
        messageHandler
            .Expect("https://api.dolby.io/v1/auth/token")
            .WithHeaders("Authorization", $"Basic {authz}")
            .WithFormData(new Dictionary<string, string>() {
                { "grant_type", "client_credentials" },
                { "expires_in", jwtToken.ExpiresIn.ToString() }
            })
            .Respond("application/json", JsonConvert.SerializeObject(jwtToken));

        JwtToken jwt;
        using (DolbyIOClient client = new DolbyIOClient(messageHandler.ToHttpClient()))
        {
            jwt = await client.Authentication.GetApiAccessTokenAsync(appKey, appSecret, jwtToken.ExpiresIn, new string[] { "comms:client_access_token:create" });
        }

        Assert.NotEqual(jwtToken, jwt);
        Assert.Equal(jwtToken.AccessToken, jwt.AccessToken);
        Assert.Equal(jwtToken.TokenType, jwt.TokenType);
        Assert.Equal(jwtToken.ExpiresIn, jwt.ExpiresIn);
        Assert.Equal(jwtToken.Scope, jwt.Scope);
    }
}
