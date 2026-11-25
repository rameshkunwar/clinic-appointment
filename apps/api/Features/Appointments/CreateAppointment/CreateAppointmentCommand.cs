namespace Clinic.API.Features.Appointments.CreateAppointment;

public record CreateAppointmentCommand(
    Guid PatientId,
    DateTime AppointmentTime,
    string Description
);
