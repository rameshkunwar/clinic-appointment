using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Clinic.API.Features.Appointments.CreateAppointment;
using Clinic.API.Features.Appointments.GetAppointment;
using Clinic.API.Features.Appointments.GetAppointments;

namespace Clinic.API.Features.Appointments;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMessageBus _messageBus;

    public AppointmentsController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateAppointmentResponse>> Create([FromBody] CreateAppointmentCommand command)
    {
        var response = await _messageBus.InvokeAsync<CreateAppointmentResponse>(command);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAppointmentResponse>> Get(Guid id)
    {
        var response = await _messageBus.InvokeAsync<GetAppointmentResponse>(new GetAppointmentQuery(id));
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreateAppointmentResponse>>> GetAll()
    {
        var response = await _messageBus.InvokeAsync<IEnumerable<CreateAppointmentResponse>>(new GetAppointmentsQuery());
        return Ok(response);
    }
}
