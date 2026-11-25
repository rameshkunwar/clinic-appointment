using Clinic.API.Data;
using Clinic.API.Features.Appointments.GetAppointments;
using Clinic.API.Features.Appointments.CreateAppointment;

namespace Clinic.API.Features.Appointments.GetAppointments;

public static class GetAppointmentsHandler
{
    public static async Task<IEnumerable<CreateAppointmentResponse>> Handle(
        GetAppointmentsQuery query,
        AppDbContext db,
        CancellationToken ct)
    {
        // TODO: Implement actual DB fetch
        return Enumerable.Empty<CreateAppointmentResponse>();
    }
}
