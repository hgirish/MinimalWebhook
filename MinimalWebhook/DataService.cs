using Dapper;
using System.Data.SqlClient;

namespace MinimalWebhook;

public class DataService : IDataService
{
    private string connString;
    public DataService(IConfiguration configuration)
    {
        connString = configuration.GetConnectionString("DefaultConnection");
    }
    public bool InsertAppointment(Appointment? appointment)
    {
        if (appointment == null)
        {
            throw new ArgumentNullException(nameof(appointment));
        }
        var sql = "Insert Appointment (Phone,Name, Appt, AppointmentDate,Created) " +
            "Values (@Phone,@Name,@Appt, @AppointmentDate,@Created)";
        var appointmentData = new { Phone = appointment.Phone, Name = appointment.Name, Appt = appointment.Appt, AppointmentDate = appointment.GetAppointment(), Created = appointment.Created };
        using var connection = new SqlConnection(connString);
        var rowsAffected = connection.Execute(sql, appointmentData);

        return rowsAffected > 0;
    }
}
