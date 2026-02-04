using ProjetoCRM.Domain.Entities;

public interface IAccountRepository
{
    Task<Account> GetAccountByIdAsync(int accountId);
    Task<IEnumerable<Account>> GetAllAccountsAsync();
    Task AddAccountAsync(Account account);
    Task UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(int accountId);
}