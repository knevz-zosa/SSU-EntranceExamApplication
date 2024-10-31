using Common.Requests;

namespace Tests.Authentication;

public class AuthenticationShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformAuthenticationMethods()
    {
        // Arrange: Authenticate first
        var loginRequest = await LoginDefault();
        Assert.NotNull(loginRequest);
        Assert.True(loginRequest.IsSuccessful);
    }
}
