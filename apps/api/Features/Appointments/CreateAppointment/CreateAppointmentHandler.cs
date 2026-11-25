using Clinic.API.Data;
using Clinic.API.Features.Appointments.CreateAppointment;
using Clinic.API.Features.Appointments.Services;

namespace Clinic.API.Features.Appointments.CreateAppointment;

public static class CreateAppointmentHandler
{
    public static async Task<CreateAppointmentResponse> Handle(
        CreateAppointmentCommand command,
        AppDbContext db,
        ICalDiyService calDiyService,
        CancellationToken ct)
    {
        // Integrate with Cal.diy via the service
        Guid appointmentId = await calDiyService.BookAppointmentAsync(
            command.PatientId,
            command.AppointmentTime,
            command.Description,
            ct
        );

        return new CreateAppointmentResponse(
            appointmentId,
            command.AppointmentTime,
            "Confirmed"
        );
    }
}
