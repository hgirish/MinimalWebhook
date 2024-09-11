using MinimalWebhook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<IReceiveWebhook, ConsoleWebhookReceiver>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/webhook", async (HttpContext context, IReceiveWebhook receiveWebhook) =>
{
    Console.WriteLine("webhook received");
    using StreamReader stream = new StreamReader(context.Request.Body);
    return await receiveWebhook.ProcessRequest(await stream.ReadToEndAsync());
});

app.Run();
public partial class Program { }