using Microsoft.AspNetCore.Mvc;
using ProjetoCRM.Api.Models.Response;
using ProjetoCRM.Api.Models.Request;
using ProjetoCRM.Domain.Entities;


[ApiController]
[Route("Specialist")]
public class SpecialistController : ControllerBase
{
    private ISpecialistApplication _specialistApplication;

    public SpecialistController(ISpecialistApplication specialistApplication)
    {
        _specialistApplication = specialistApplication;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> Add([FromBody] SpecialistAdd specialistDTO)
    {
        try
        {
            var specialist = new Specialist()
            {
                Name = specialistDTO.Name,
                UserId = specialistDTO.UserId
            };

            var idSpecialist = await _specialistApplication.AddAsync(specialist);

            return Ok($"ID: {idSpecialist}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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
    [Route("GetById/{specialistId}")]
    public async Task<ActionResult> GetById([FromRoute] int specialistId)
    {
        try
        {
            var specialist = await _specialistApplication.GetByIdAsync(specialistId);

            var specialistResponse = new SpecialistResponse()
            {
                ID = specialist.ID,
                Name = specialist.Name,
                UserId = specialist.UserId
            };

            return Ok(specialistResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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
    [Route("GetByName")]
    public async Task<ActionResult> GetByName([FromQuery] string name)
    {
        try
        {
            var specialist = await _specialistApplication.GetByNameAsync(name);

            var specialistResponse = new SpecialistResponse()
            {
                ID = specialist.ID,
                Name = specialist.Name,
                UserId = specialist.UserId
            };

            return Ok(specialistResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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
    [Route("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var specialists = await _specialistApplication.GetAllAsync();

            var specialistsResponse = specialists.Select(s => new SpecialistResponse()
            {
                ID = s.ID,
                Name = s.Name,
                UserId = s.UserId
            });

            return Ok(specialistsResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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

    [HttpPut]
    [Route("Update")]
    public async Task<ActionResult> Update([FromBody] SpecialistUpdate specialistUpdate)
    {
        try
        {
            var specialist = await _specialistApplication.GetByIdAsync(specialistUpdate.Id);

            specialist.Name = specialistUpdate.Name;
            specialist.UserId = specialistUpdate.UserId;

            await _specialistApplication.UpdateAsync(specialist);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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

    [HttpDelete]
    [Route("Delete/{specialistId}")]
    public async Task<ActionResult> Delete([FromRoute] int specialistId)
    {
        try
        {
            await _specialistApplication.DeleteAsync(specialistId);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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

    [HttpPut]
    [Route("Restore/{specialistId}")]
    public async Task<ActionResult> Restore([FromRoute] int specialistId)
    {
        try
        {
            await _specialistApplication.RestoreAsync(specialistId);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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