using ProjetoCRM.Domain.Entities;

public interface ISpecialistRepository
{
    Task<int> AddAsync(Specialist specialist);
    Task<Specialist> GetByIdAsync(int idSpecialist);
    Task<Specialist> GetByNameAsync(string nameSpecialist);
    Task<IEnumerable<Specialist>> GetAllAsync();
    Task UpdateAsync(Specialist specialist);
    Task DeleteAsync(Specialist specialist);
}
