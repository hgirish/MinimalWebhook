using Dapper;
using MinimalWebhook.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MinimalWebhook;

public class DataService : IDataService
{
    private string connString;
    public DataService(IConfiguration configuration)
    {
        connString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }
    public bool InsertAppointment(Appointment? appointment)
    {
        if (appointment == null)
        {
            throw new ArgumentNullException(nameof(appointment));
        }
        var sql = "Insert Appointment (Phone,Name, Appt,Status, AppointmentDate,Created) " +
            "Values (@Phone,@Name,@Appt, @Status, @AppointmentDate,@Created)";
        var appointmentData = new { Phone = appointment.Phone, Name = appointment.Name, Appt = appointment.Appt, Status = appointment.Status, AppointmentDate = appointment.GetAppointment(), Created = appointment.Created };
        using var connection = new SqlConnection(connString);
        var rowsAffected = connection.Execute(sql, appointmentData);

        return rowsAffected > 0;
    }

    public bool InsertWhatsAppNotification(WhatsAppNotification notification)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof (notification));
        }

        var sql = "Insert Notifications (Timestamp, Description,Code,DeliveryChannel, AdditionalInfo,Destination, DestinationType, IdentityKeyHash, DeliveryStatus,Subtid, TransId,CallbackData, CorrelationId   )" +
            "Values (@Timestamp, @Description,@Code,@DeliveryChannel, @AdditionalInfo,@Destination, @DestinationType, @IdentityKeyHash, @DeliveryStatus,@Subtid, @TransId,@CallbackData, @CorrelationId) ";
        var info = notification?.deliveryInfoNotification?.deliveryInfo;
        var notificationData = new
        {
            Timestamp = info?.timeStamp,
            Description = info?.Description,
            Code = info?.code,
            DeliveryChannel = info?.deliveryChannel,
            AdditionalInfo = info?.additionalInfo,
            Destination = info?.destination,
            DestinationType = info?.destinationType,
            IdentityKeyHash = info?.identityKeyHash,
            DeliveryStatus = info?.deliveryStatus,
            SubtId = notification?.deliveryInfoNotification?.subtid,
            TransId = notification?.deliveryInfoNotification?.transid,
            CallbackData = notification?.deliveryInfoNotification?.callbackData,
            CorrelationId = notification?.deliveryInfoNotification?.correlationid

        };
        using var connection = new SqlConnection(connString);
        var rowsAffected = connection.Execute(sql, notificationData);

        return rowsAffected > 0;
    }

    public List<Person> GetPersonByPhone(string phone)
    {
        phone = NumbersOnly(phone);
        using var connection = new SqlConnection(connString);
        var persons = connection.Query<Person>("Select * from Persons").ToList();
        var person = persons.Where(p => !string.IsNullOrEmpty(p.MobileNumber) &&  NumbersOnly(p.MobileNumber) == phone);
        return person.ToList();
    }
    public string NumbersOnly(string phone) {
        if (string.IsNullOrEmpty(phone))
        {
            return "";
        }
        phone = phone.Trim();
        phone = Regex.Replace(phone, "[^0-9]", "");
        return phone;
    }

    public void PatchPerson(int id, Person model)
    {
        using var connection = new SqlConnection(connString);
        var sql = "Update Persons set VideoCallScheduled = @VideoCallScheduled, ModifiedAt = @ModifiedAt where Id = @Id";
        var data = new { Id = id, VideoCallScheduled = model.videoCallScheduled, ModifiedAt = DateTime.UtcNow };
        connection.Execute(sql, data);
        return;

    }
}
