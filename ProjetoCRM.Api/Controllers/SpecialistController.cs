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
}