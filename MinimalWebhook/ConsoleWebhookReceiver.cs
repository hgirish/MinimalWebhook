using MinimalWebhook;
using System.Text.Json;

namespace MinimalWebhook;

public class ConsoleWebhookReceiver : IReceiveWebhook
{
    private readonly IDataService _dataService;

    public ConsoleWebhookReceiver(IDataService dataService)
    {
        _dataService = dataService;
    }
    public async Task<string> ProcessRequest(string requestBody)
    {
        Console.WriteLine($"Request Body: {requestBody}");
        Appointment appointment = JsonSerializer.Deserialize<Appointment>(requestBody);
        Console.WriteLine(appointment.Name);
        var success = _dataService.InsertAppointment(appointment);
        Console.WriteLine($"Data inserted: {success}");
        return $@"{{""message"" : ""Thanks {appointment.Name}! We got your webhook. Appointment: {appointment.GetAppointment()}""}}";
    }
}
