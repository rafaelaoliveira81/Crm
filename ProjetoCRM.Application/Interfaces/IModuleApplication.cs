using ProjetoCRM.Domain.Entities;

public interface IModuleApplication
{
    Task<int> AddAsync(Module module);
    Task<Module> GetByIdAsync(int moduleId);
    Task<IEnumerable<Module>> GetAllAsync();
    Task UpdateAsync(Module module);
    Task DeleteAsync(int moduleId);
}