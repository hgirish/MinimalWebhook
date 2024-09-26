namespace MinimalWebhook.Models;

public class MessageResponse
{
    public string AcceptedTime { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}