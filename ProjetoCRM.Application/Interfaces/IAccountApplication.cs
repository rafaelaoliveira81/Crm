using ProjetoCRM.Domain.Entities;

public interface IAccountApplication
{
    Task<int> AddAsync(Account account);
    Task<Account> GetByIdAsync(int accountId);
    Task<IEnumerable<Account>> GetAllAsync();
    Task UpdateAsync(Account account);
    Task DeleteAsync(int accountId);
    Task RestoreAsync(int accountId);
}