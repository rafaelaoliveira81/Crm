using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(ProjetoCRMContext context) : base(context)
    {
    }

    public async Task<int> AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client.ID;
    }

    public async Task<Client> GetByIdAsync(int idClient)
    {
        return await _context.Clients.FindAsync(idClient);
    }
    public async Task<Client> GetByNameAsync(string nameClient)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Name == nameClient);
    }
    public async Task<Client> GetByPhoneNumberAsync(string phoneNumberClient)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumberClient);
    }

    public async Task<Client> GetByEmailAsync(string emailClient)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Email == emailClient);
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.Where(c => c.IsActive == true).ToListAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}