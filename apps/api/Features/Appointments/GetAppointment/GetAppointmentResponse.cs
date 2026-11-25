namespace Clinic.API.Features.Appointments.GetAppointment;

public record GetAppointmentResponse(Guid AppointmentId, DateTime AppointmentTime, string Status);
