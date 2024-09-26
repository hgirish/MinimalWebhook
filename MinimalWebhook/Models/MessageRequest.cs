namespace MinimalWebhook.Models;

public class MessageRequest
{
    public string? ContentType { get; set; } = string.Empty;
    public string? From { get; set; } = string.Empty;
    public string? To { get; set; } = string.Empty;
    public string? Content { get; set; } = string.Empty;

}// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);




