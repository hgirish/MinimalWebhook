
using MinimalWebhook.Models;

public interface IReceiveWhatsAppNotification
{
    string ProcessRequest(string requestBody);
}
