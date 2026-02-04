using ProjetoCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(ProjetoCRMContext context) : base(context)
    {
    }
    public async Task<Account> GetAccountByIdAsync(int accountId)
    {
        return await _context.Accounts.FindAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task AddAccountAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAccountAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await GetAccountByIdAsync(accountId);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}
