using ProjetoCRM.Domain.Entities;
public interface IPlanRepository
{
    Task<int> AddAsync(Plan plan);
    Task<Plan> GetByIdAsync(int idPlan);
    Task<IEnumerable<Plan>> GetAllAsync();
    Task UpdateAsync(Plan plan);
    Task DeleteAsync(Plan plan);
}