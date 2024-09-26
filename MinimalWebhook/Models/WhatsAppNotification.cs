namespace MinimalWebhook.Models;

public class DeliveryInfo
{
    public string? deliveryChannel { get; set; }
    public string? Description { get; set; }
    public string? destinationType { get; set; }
    public DateTime? timeStamp { get; set; }
    public string? code { get; set; }
    public string? additionalInfo { get; set; }
    public string? deliveryStatus { get; set; }
    public string? destination { get; set; }
    public string? identityKeyHash { get; set; }
}

public class DeliveryInfoNotification
{
    public string? subtid { get; set; }
    public DeliveryInfo? deliveryInfo { get; set; }
    public string? correlationid { get; set; }
    public string? callbackData { get; set; }
    public string? transid { get; set; }
}
public class WhatsAppNotification
{
    public DeliveryInfoNotification deliveryInfoNotification { get; set; }
}



