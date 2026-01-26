using Microsoft.AspNetCore.Mvc;
using ProjetoCRM.Api.Models.Response;
using ProjetoCRM.Api.Models.Request;
using ProjetoCRM.Domain.Entities;

[ApiController]
[Route("Client")]
public class ClientController : ControllerBase
{
    private IClientApplication _clientApplication;

    public ClientController(IClientApplication clientApplication)
    {
        _clientApplication = clientApplication;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> Add([FromBody] ClientAdd client)
    {
        try
        {
            var clientRepository = new Client()
            {
                Name = client.Name,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };

            var idClient = await _clientApplication.AddAsync(clientRepository);

            return Ok($"ID: {idClient}");
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
    [Route("GetById/{idClient}")]
    public async Task<ActionResult> GetById([FromRoute] int idClient)
    {
        try
        {
            var clientRepository = await _clientApplication.GetByIdAsync(idClient);
            var clientResponse = new ClientResponse()
            {
                ID = clientRepository.ID,
                Name = clientRepository.Name,
                Email = clientRepository.Email,
                PhoneNumber = clientRepository.PhoneNumber
            };

            return Ok(clientResponse);
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
    [Route("GetByEmail")]
    public async Task<ActionResult> GetByEmail([FromQuery] string emailClient)
    {
        try
        {
            var clientRepository = await _clientApplication.GetByEmailAsync(emailClient);
            var clientResponse = new ClientResponse()
            {
                ID = clientRepository.ID,
                Name = clientRepository.Name,
                Email = clientRepository.Email,
                PhoneNumber = clientRepository.PhoneNumber
            };

            return Ok(clientResponse);
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
    public async Task<ActionResult> GetByName([FromQuery] string nameClient)
    {
        try
        {
            var clientRepository = await _clientApplication.GetByNameAsync(nameClient);
            var clientResponse = new ClientResponse()
            {
                ID = clientRepository.ID,
                Name = clientRepository.Name,
                Email = clientRepository.Email,
                PhoneNumber = clientRepository.PhoneNumber
            };

            return Ok(clientResponse);
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
    [Route("GetByPhoneNumber")]
    public async Task<ActionResult> GetByPhoneNumber([FromQuery] string phoneNumberClient)
    {
        try
        {
            var clientRepository = await _clientApplication.GetByPhoneNumberAsync(phoneNumberClient);
            var clientResponse = new ClientResponse()
            {
                ID = clientRepository.ID,
                Name = clientRepository.Name,
                Email = clientRepository.Email,
                PhoneNumber = clientRepository.PhoneNumber
            };

            return Ok(clientResponse);
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
            var clients = await _clientApplication.GetAllAsync();

            var clientsResponse = clients.Select(c => new ClientResponse()
            {
                ID = c.ID,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            });

            return Ok(clientsResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<ActionResult> Update([FromBody] ClientUpdate client)
    {
        try
        {
            var clientRepository = await _clientApplication.GetByIdAsync(client.ID);

            clientRepository.Name = client.Name;
            clientRepository.Email = client.Email;
            clientRepository.PhoneNumber = client.PhoneNumber;

            await _clientApplication.UpdateAsync(clientRepository);

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
    [Route("Delete/{idClient}")]
    public async Task<ActionResult> Delete([FromRoute] int idClient)
    {
        try
        {
            await _clientApplication.DeleteAsync(idClient);

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

    [HttpPut]
    [Route("Restore/{idClient}")]
    public async Task<ActionResult> Restore([FromRoute] int idClient)
    {
        try
        {
            await _clientApplication.RestoreAsync(idClient);

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