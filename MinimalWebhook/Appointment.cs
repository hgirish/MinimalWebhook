namespace MinimalWebhook;

public class Appointment
{
    public string? Phone { get; set; }
    public string? Name { get; set; }
    public string? Appt { get; set; }
    public DateTime? GetAppointment()
    {
        DateTime.TryParse(Appt, out DateTime date);
        return date;
    }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
