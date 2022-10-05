using System.Text;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace DolbyIO.Rest.Tests.Communications;

[Collection("Communications")]
public class AuthenticationTests
{
    [Fact]
    public async Task Test_Authentication_GetClientAccessTokenAsync()
    {
        var jwtToken = new JwtToken
        {
            AccessToken = "abcdef",
            TokenType = "Bearer",
            ExpiresIn = 123
        };

        const string appKey = "app_key";
        const string appSecret = "app_secret";
        string authz = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{appKey}:{appSecret}"));

        var messageHandler = new MockHttpMessageHandler();
        messageHandler
            .Expect(Urls.SESSION_BASE_URL + "/v1/oauth2/token")
            .WithHeaders("Authorization", $"Basic {authz}")
            .WithFormData(new Dictionary<string, string>() {
                { "grant_type", "client_credentials" },
                { "expires_in", jwtToken.ExpiresIn.ToString() }
            })
            .Respond("application/json", JsonConvert.SerializeObject(jwtToken));

        JwtToken jwt;
        using (DolbyIOClient client = new DolbyIOClient(messageHandler.ToHttpClient()))
        {
            jwt = await client.Communications.Authentication.GetClientAccessTokenAsync(appKey, appSecret, jwtToken.ExpiresIn);
        }

        Assert.NotEqual(jwtToken, jwt);
        Assert.Equal(jwtToken.AccessToken, jwt.AccessToken);
        Assert.Equal(jwtToken.TokenType, jwt.TokenType);
        Assert.Equal(jwtToken.ExpiresIn, jwt.ExpiresIn);
    }
}
