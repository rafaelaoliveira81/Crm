using ProjetoCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class ModuleRepository : BaseRepository, IModuleRepository
{
    public ModuleRepository(ProjetoCRMContext context) : base(context)
    {
    }

    public async Task<int> AddAsync(Module module)
    {
        await _context.Modules.AddAsync(module);
        await _context.SaveChangesAsync();

        return module.Id;
    }

    public async Task DeleteAsync(Module module)
    {
        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules.ToListAsync();
    }

    public async Task<Module> GetByIdAsync(int id)
    {
        return await _context.Modules.FindAsync(id);
    }

    public async Task UpdateAsync(Module module)
    {
        _context.Modules.Update(module);
        await _context.SaveChangesAsync();
    }
}