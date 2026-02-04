using ProjetoCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class ModuleRepository : BaseRepository, IModuleRepository
{
    public ModuleRepository(ProjetoCRMContext context) : base(context)
    {
    }

    public Task AddAsync(Module module)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Module module)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Module>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Module> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Module module)
    {
        throw new NotImplementedException();
    }
}