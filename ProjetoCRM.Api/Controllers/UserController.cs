using Microsoft.AspNetCore.Mvc;
using ProjetoCRM.Api.Models.Response;
using ProjetoCRM.Api.Models.Request;
using ProjetoCRM.Domain.Entities;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private IUserApplication _userApplication;

    public UserController(IUserApplication userApplication)
    {
        _userApplication = userApplication;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> Add([FromBody] UserAdd user)
    {
        try
        {
            var userRepository = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            var idUser = await _userApplication.AddAsync(userRepository);

            return Ok($"ID: {idUser}");
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
    [Route("GetById/{idUser}")]
    public async Task<ActionResult> GetById([FromRoute] int idUser)
    {
        try
        {
            var userRepository = await _userApplication.GetByIdAsync(idUser);

            var userResponse = new UserResponse()
            {
                IdUser = userRepository.ID,
                Name = userRepository.Name,
                Email = userRepository.Email
            };

            return Ok(userResponse);
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
    public async Task<ActionResult> GetByEmail([FromQuery] string emailUser)
    {
        try
        {
            var userRepository = await _userApplication.GetByEmailAsync(emailUser);

            var userResponse = new UserResponse()
            {
                IdUser = userRepository.ID,
                Name = userRepository.Name,
                Email = userRepository.Email
            };

            return Ok(userResponse);
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
            var users = await _userApplication.GetAllAsync();

            var usersResponse = users.Select(u => new UserResponse()
            {
                IdUser = u.ID,
                Name = u.Name,
                Email = u.Email
            });

            return Ok(usersResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<ActionResult> Update([FromBody] UserUpdate user)
    {
        try
        {
            var userRepository = await _userApplication.GetByIdAsync(user.IdUser);

            userRepository.Name = user.Name;
            userRepository.Email = user.Email;

            await _userApplication.UpdateAsync(userRepository);

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

    [HttpPut]
    [Route("UpdatePassword")]
    public async Task<ActionResult> UpdatePassword([FromBody] UserUpdatePassword user)
    {
        try
        {
            var userEntity = new User()
            {
                ID = user.IdUser,
                Password = user.NewPassword
            };

            await _userApplication.UpdatePasswordAsync(userEntity, user.CurrentPassword);

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
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("Delete/{idUser}")]
    public async Task<ActionResult> Delete([FromRoute] int idUser)
    {
        try
        {
            await _userApplication.DeleteAsync(idUser);

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
    [Route("Restore/{idUser}")]
    public async Task<ActionResult> Restore([FromRoute] int idUser)
    {
        try
        {
            await _userApplication.RestoreAsync(idUser);

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