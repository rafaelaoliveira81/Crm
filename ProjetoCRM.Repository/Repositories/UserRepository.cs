using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class UserRepository : IUserRepository
{
    private readonly ProjetoCRMContext _context;

    public UserRepository(ProjetoCRMContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.ID;
    }

    public async Task<User> GetByIdAsync(int idUser)
    {
        return await _context.Users.FindAsync(idUser);
    }

    public async Task<User> GetByEmailAsync(string emailUser)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == emailUser);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

}