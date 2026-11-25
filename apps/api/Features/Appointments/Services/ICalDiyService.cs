using Clinic.API.Features.Appointments.CreateAppointment;

namespace Clinic.API.Features.Appointments.Services;

public interface ICalDiyService
{
    /// <summary>
    /// Book an appointment via the Cal.diy engine.
    /// </summary>
    /// <returns>The unique ID of the created appointment.</returns>
    Task<Guid> BookAppointmentAsync(Guid patientId, DateTime appointmentTime, string description, CancellationToken ct);
}
