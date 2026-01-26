using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class SpecialistRepository : BaseRepository, ISpecialistRepository
{
    public SpecialistRepository(ProjetoCRMContext context) : base(context)
    {
    }

    public async Task<int> AddAsync(Specialist specialist)
    {
        _context.Specialists.Add(specialist);
        await _context.SaveChangesAsync();

        return specialist.ID;
    }

    public async Task<Specialist> GetByIdAsync(int idSpecialist)
    {
        return await _context.Specialists.FindAsync(idSpecialist);
    }

    public async Task<Specialist> GetByNameAsync(string nameSpecialist)
    {
        return await _context.Specialists.FirstOrDefaultAsync(u => u.Name == nameSpecialist);
    }

    public async Task<IEnumerable<Specialist>> GetAllAsync()
    {
        return await _context.Specialists.Where(s => s.IsActive == true).ToListAsync();
    }

    public async Task UpdateAsync(Specialist specialist)
    {
        _context.Specialists.Update(specialist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Specialist specialist)
    {
        _context.Specialists.Remove(specialist);
        await _context.SaveChangesAsync();
    }
}