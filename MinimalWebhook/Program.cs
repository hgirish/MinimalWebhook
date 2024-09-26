using Microsoft.AspNetCore.Http.HttpResults;
using MinimalWebhook;
using MinimalWebhook.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<IReceiveWebhook, ConsoleWebhookReceiver>();
builder.Services.AddSingleton<IReceiveWhatsAppNotification, ReceiveWhatsAppNotification>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () =>
{
    return "Hello World!";
});
app.MapPost("/webhook", async (HttpContext context, IReceiveWebhook receiveWebhook) =>
{
    Console.WriteLine("webhook received");
    using StreamReader stream = new StreamReader(context.Request.Body);
    return  receiveWebhook.ProcessRequest(await stream.ReadToEndAsync());
});
app.MapPost("/WhatsAppNotification", async (HttpContext context, IReceiveWhatsAppNotification receiver) =>
{
    
    using StreamReader stream = new StreamReader(context.Request.Body);
    return receiver.ProcessRequest(await stream.ReadToEndAsync());

   
});

app.Run();
public partial class Program { }