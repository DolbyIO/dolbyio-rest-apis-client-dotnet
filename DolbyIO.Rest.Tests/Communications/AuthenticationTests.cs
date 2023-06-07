using DolbyIO.Rest.Models;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace DolbyIO.Rest.Tests.Communications;

[Collection("Communications")]
public class AuthenticationTests
{
    [Fact]
    public async Task Test_Authentication_GetClientAccessTokenV2Async()
    {
        var jwtToken = new JwtToken
        {
            AccessToken = "abcdef",
            TokenType = "Bearer",
            ExpiresIn = 123
        };

        var messageHandler = new MockHttpMessageHandler();
        messageHandler
            .Expect("https://comms.api.dolby.io/v2/client-access-token")
            .WithHeaders("Authorization", $"Bearer {jwtToken.AccessToken}")
            .WithFormData(new Dictionary<string, string>() {
                { "grant_type", "client_credentials" },
                { "expires_in", jwtToken.ExpiresIn.ToString() },
                { "sessionScope", "*" }
            })
            .Respond("application/json", JsonConvert.SerializeObject(jwtToken));

        JwtToken jwt;
        using (DolbyIOClient client = new DolbyIOClient(messageHandler.ToHttpClient()))
        {
            jwt = await client.Communications.Authentication.GetClientAccessTokenV2Async(jwtToken, new string[] {"*"}, expiresIn: 123);
        }

        Assert.NotEqual(jwtToken, jwt);
        Assert.Equal(jwtToken.AccessToken, jwt.AccessToken);
        Assert.Equal(jwtToken.TokenType, jwt.TokenType);
        Assert.Equal(jwtToken.ExpiresIn, jwt.ExpiresIn);
    }
}
