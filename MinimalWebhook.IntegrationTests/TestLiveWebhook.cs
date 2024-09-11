using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MinimalWebhook.IntegrationTests;

public class LiveWebhookTests
{
    [Fact]
    public async Task TestLiveWebhook()
    {
        var client = new HttpClient();
        var response = await client.PostAsync(
            "https://localhost:7267/webhook",
            new StringContent("Hi"));
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("{\"message\" : \"Thanks! We got your webhook\"}", responseBody);

    }
}
