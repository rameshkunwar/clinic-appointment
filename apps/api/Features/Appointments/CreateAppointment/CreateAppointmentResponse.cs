namespace Clinic.API.Features.Appointments.CreateAppointment;

public record CreateAppointmentResponse(
    Guid AppointmentId,
    DateTime AppointmentTime,
    string Status
);
