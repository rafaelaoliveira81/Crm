using Microsoft.AspNetCore.Mvc;
using ProjetoCRM.Api.Models.Response;
using ProjetoCRM.Api.Models.Request;
using ProjetoCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[ApiController]
[Route("Appointment")]
public class AppointmentController : ControllerBase
{
    private IAppointmentApplication _appointmentApplication;
    public AppointmentController(IAppointmentApplication appointmentApplication)
    {
        _appointmentApplication = appointmentApplication;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> Add([FromBody] AppointmentAdd appointment)
    {
        try
        {
            var appointmentRepository = new Appointment()
            {
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Status = appointment.Status,
                Description = appointment.Description,
                ClientId = appointment.ClientId,
                SpecialistId = appointment.SpecialistId,
                UserId = appointment.UserId
            };

            var idAppointment = await _appointmentApplication.AddAsync(appointmentRepository);

            return Ok($"ID: {idAppointment}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var appointments = await _appointmentApplication.GetAllAsync();

            var appointmentsResponse = appointments.Select(appointment => new AppointmentResponse
            {
                ID = appointment.ID,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Status = appointment.Status,
                Description = appointment.Description,
                ClientId = (int)appointment.ClientId,
                SpecialistId = (int)appointment.SpecialistId,
                UserId = (int)appointment.UserId
            }).ToList();

            return Ok(appointmentsResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetAllBySpecialistIdAsync/{specialistId}")]
    public async Task<ActionResult> GetAllBySpecialistIdAsync(int specialistId)
    {
        try
        {
            var appointments = await _appointmentApplication.GetAllBySpecialistIdAsync(specialistId);

            var appointmentsResponse = appointments.Select(appointment => new AppointmentResponse
            {
                ID = appointment.ID,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Status = appointment.Status,
                Description = appointment.Description,
                ClientId = (int)appointment.ClientId,
                SpecialistId = (int)appointment.SpecialistId,
                UserId = (int)appointment.UserId
            }).ToList();

            return Ok(appointmentsResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetAllByClientIdAsync/{clientId}")]
    public async Task<ActionResult> GetAllByClientIdAsync(int clientId)
    {
        try
        {
            var appointments = await _appointmentApplication.GetAllByClientIdAsync(clientId);

            var appointmentsResponse = appointments.Select(appointment => new AppointmentResponse
            {
                ID = appointment.ID,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Status = appointment.Status,
                Description = appointment.Description,
                ClientId = (int)appointment.ClientId,
                SpecialistId = (int)appointment.SpecialistId,
                UserId = (int)appointment.UserId
            }).ToList();

            return Ok(appointmentsResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetById/{appointmentId}")]
    public async Task<ActionResult> GetById(int appointmentId)
    {
        try
        {
            var appointment = await _appointmentApplication.GetByIdAsync(appointmentId);

            if (appointment == null)
                return NotFound();

            var appointmentResponse = new AppointmentResponse
            {
                ID = appointment.ID,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Status = appointment.Status,
                Description = appointment.Description,
                ClientId = (int)appointment.ClientId,
                SpecialistId = (int)appointment.SpecialistId,
                UserId = (int)appointment.UserId
            };

            return Ok(appointmentResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<ActionResult> Update([FromBody] AppointmentUpdate appointmentUpdate)
    {
        try
        {
            var appointment = new Appointment()
            {
                ID = appointmentUpdate.ID,
                StartAt = appointmentUpdate.StartAt,
                EndAt = appointmentUpdate.EndAt,
                Status = appointmentUpdate.Status,
                Description = appointmentUpdate.Description,
                ClientId = appointmentUpdate.ClientId,
                SpecialistId = appointmentUpdate.SpecialistId,
                UserId = appointmentUpdate.UserId
            };

            await _appointmentApplication.UpdateAsync(appointment);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("Delete/{appointmentId}")]
    public async Task<ActionResult> Delete(int appointmentId)
    {
        try
        {
            await _appointmentApplication.DeleteAsync(appointmentId);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}