using Clinic.API.Data;
using Clinic.API.Features.Appointments.GetAppointment;

namespace Clinic.API.Features.Appointments.GetAppointment;

public static class GetAppointmentHandler
{
    public static async Task<GetAppointmentResponse> Handle(
        GetAppointmentQuery query,
        AppDbContext db,
        CancellationToken ct)
    {
        // TODO: Implement actual DB fetch
        return new GetAppointmentResponse(query.AppointmentId, DateTime.UtcNow, "Stub");
    }
}
