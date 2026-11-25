using Clinic.API.Features.Appointments.CreateAppointment;
using Microsoft.Extensions.Logging;

namespace Clinic.API.Features.Appointments.Services;

public class CalDiyService : ICalDiyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CalDiyService> _logger;

    public CalDiyService(HttpClient httpClient, ILogger<CalDiyService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Guid> BookAppointmentAsync(Guid patientId, DateTime appointmentTime, string description, CancellationToken ct)
    {
        _logger.LogInformation("Booking appointment for patient {PatientId} at {AppointmentTime}", patientId, appointmentTime);

        // TODO: Implement actual HTTP call to booking.clinic.com
        // The endpoint and payload will depend on the Cal.diy API specification.

        // For now, we simulate a successful booking.
        await Task.Delay(100, ct);

        return Guid.NewGuid();
    }
}
