
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using System.Net;

namespace MinimalWebhook.IntegrationTests;

public class TestWebhook
{
    [Fact]
    public async void TestReceivingHook()
    {
        var fakeReceiver = new FakeWebhookReceiver();

        await WithTestServer(async (c, s) =>
        {
            var response = await c.PostAsync("webhook", new StringContent("Hi"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseText = await response.Content.ReadAsStringAsync();

            Assert.Equal("Hello back", responseText);

            Assert.Equal("Hi", fakeReceiver.Receipts.First());
        }, s=> s.AddSingleton((IReceiveWebhook)fakeReceiver));
    }

    private async Task WithTestServer(
        Func<HttpClient, IServiceProvider, Task> test,
        Action<IServiceCollection> configureServices
        )
    {
        await using var application =
            new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => configureServices(services));
            });

        using var client = application.CreateClient();
        await test(client, application.Services);
    }
}

public class FakeWebhookReceiver : IReceiveWebhook
{
    public List<string> Receipts = new List<string>();
    public async Task<string> ProcessRequest(string requestBody)
    {
        Receipts.Add(requestBody);
        return "Hello back";
    }
}