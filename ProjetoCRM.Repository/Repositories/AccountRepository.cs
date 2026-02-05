using ProjetoCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(ProjetoCRMContext context) : base(context)
    {
    }
    public async Task<Account> GetByIdAsync(int accountId)
    {
        return await _context.Accounts.FindAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<int> AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return account.Id;
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Account account)
    {
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }
}
