using ProjetoCRM.Domain.Entities;

public interface IPlanApplication
{
    Task<int> AddAsync(Plan plan);
    Task<Plan> GetByIdAsync(int planId);
    Task<IEnumerable<Plan>> GetAllAsync();
    Task UpdateAsync(Plan plan);
    Task DeleteAsync(int planId);
}