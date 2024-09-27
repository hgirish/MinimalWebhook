using MinimalWebhook.Models;

namespace MinimalWebhook
{
    public interface IDataService
    {
        bool InsertAppointment(Appointment? appointment);
        bool InsertWhatsAppNotification(WhatsAppNotification notification);
        List<Person> GetPersonByPhone(string phone);
    }
}