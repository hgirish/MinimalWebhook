
using MinimalWebhook;
using MinimalWebhook.Models;
using System.Text.Json;

public class ReceiveWhatsAppNotification : IReceiveWhatsAppNotification
{
    private readonly IDataService _dataService;

    public ReceiveWhatsAppNotification(IDataService dataService)
    {
        _dataService = dataService;
    }
    public string ProcessRequest(string  requestBody)
    {
        Console.WriteLine($"Request Body: {requestBody}");
        var notification = JsonSerializer.Deserialize<WhatsAppNotification>(requestBody);

        var success =  _dataService.InsertWhatsAppNotification(notification);
        return success.ToString();
    }
}