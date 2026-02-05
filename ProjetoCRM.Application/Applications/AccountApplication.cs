using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Domain.Enuns;

public class AccountApplication : IAccountApplication
{
    private readonly IAccountRepository _accountRepository;
    public AccountApplication(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<int> AddAsync(Account account)
    {
        ValidateAccountInformation(account);

        return await _accountRepository.AddAsync(account);
    }

    public async Task DeleteAsync(int accountId)
    {
        var account = await ValidateAccountExistsByIdAsync(accountId);
        account.Deletar();

        await _accountRepository.UpdateAsync(account);
    }

    public async Task RestoreAsync(int accountId)
    {
        var account = await ValidateAccountExistsByIdAsync(accountId);
        account.Restore();

        await _accountRepository.UpdateAsync(account);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<Account> GetByIdAsync(int accountId)
    {
        return await _accountRepository.GetByIdAsync(accountId);
    }

    public async Task UpdateAsync(Account account)
    {
        ValidateAccountInformation(account);
        await _accountRepository.UpdateAsync(account);
    }

    #region Uteis
    private static void ValidateAccountInformation(Account account)
    {
        if (account == null)
            throw new ArgumentException("Conta não pode ser vazia.");

        if (string.IsNullOrEmpty(account.Name))
            throw new ArgumentException("O nome da conta deve ser informado.");

        if (account.PlanId <= 0)
            throw new ArgumentException("O plano da conta deve ser informado.");

        if (!Enum.IsDefined(typeof(AccountStatus), account.Status))
            throw new ArgumentException("O status da conta deve ser informado.");

        if (account.ContractValue <= 0)
            throw new ArgumentException("O valor do contrato deve ser maior que zero.");

        if (account.HealthScore < 0 || account.HealthScore > 100)
            throw new ArgumentException("O health score deve ser entre 0 e 100.");
    }
    private async Task<Account> ValidateAccountExistsByIdAsync(int idAccountDTO)
    {
        var accountEntity = await _accountRepository.GetByIdAsync(idAccountDTO);

        if (accountEntity == null)
            throw new KeyNotFoundException("Conta não encontrada.");

        return accountEntity;
    }
    #endregion
}