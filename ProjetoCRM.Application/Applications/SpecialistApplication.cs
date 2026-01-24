using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Application;

public class SpecialistApplication : ISpecialistApplication
{
    private readonly ISpecialistApplication _specialistApplication;

    public SpecialistApplication(ISpecialistApplication specialistApplication)
    {
        _specialistApplication = specialistApplication;
    }

    public Task<int> AddAsync(Specialist specialistDTO)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int idSpecialistDTO)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Specialist>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Specialist> GetByIdAsync(int idSpecialistDTO)
    {
        throw new NotImplementedException();
    }

    public Task<Specialist> GetByNameAsync(string nameSpecialistDTO)
    {
        throw new NotImplementedException();
    }

    public Task RestoreAsync(int idSpecialistDTO)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Specialist specialistDTO)
    {
        throw new NotImplementedException();
    }
}