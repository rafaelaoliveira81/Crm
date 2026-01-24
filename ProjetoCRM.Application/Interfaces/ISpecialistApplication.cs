using ProjetoCRM.Domain.Entities;

public interface ISpecialistApplication
{
    Task<int> AddAsync(Specialist specialistDTO);
    Task<Specialist> GetByIdAsync(int idSpecialistDTO);
    Task<Specialist> GetByNameAsync(string nameSpecialistDTO);
    Task<IEnumerable<Specialist>> GetAllAsync();
    Task UpdateAsync(Specialist specialistDTO);
    Task DeleteAsync(int idSpecialistDTO);
    Task RestoreAsync(int idSpecialistDTO);
}
