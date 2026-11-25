using FluentValidation;

namespace Clinic.API.Features.Appointments.CreateAppointment;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.AppointmentTime).GreaterThan(DateTime.UtcNow);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
    }
}
