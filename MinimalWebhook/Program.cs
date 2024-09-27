using Microsoft.AspNetCore.Http.HttpResults;
using MinimalWebhook;
using Microsoft.AspNetCore.OpenApi;
using MinimalWebhook.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<IReceiveWebhook, ConsoleWebhookReceiver>();
builder.Services.AddSingleton<IReceiveWhatsAppNotification, ReceiveWhatsAppNotification>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

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

app.MapPersonEndpoints();

app.Run();
public partial class Program { }
